using AlphaX.Parserz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaX.FormulaEngine
{
    internal class Evaluator : IEvaluator
    {
        private IFormulaStore _formulaStore;

        internal LogicalOperators SupportedLogicalOperators { get; set; }

        public Evaluator(IFormulaStore formulaStore)
        {
            _formulaStore = formulaStore;
        }

        public async Task<object> Evaluate(IParserResult result, IEngineContext context)
        {
            if(result is ArrayResult nodes)
            {
                List<object> arguments = new List<object>();
                Dictionary<int, Task<object>> pendingTasks = new Dictionary<int, Task<object>>();

                FormulaBase formula = null;

                for (int index = 0; index < nodes.Value.Length; index++)
                {
                    var item = nodes.Value[index];

                    if (item.Type == FormulaParserResultType.FormulaName)
                    {
                        var formulaName = item.Value.ToString();

                        if (!_formulaStore.Contains(formulaName))
                            throw new EvaluationException($"Invalid formula '{formulaName}'");

                        formula = (_formulaStore as FormulaStore).Get(formulaName);
                        continue;
                    }

                    if (item.Type == ParserResultType.Array 
                        || item.Type == FormulaParserResultType.CustomName 
                        || item.Type == FormulaParserResultType.Condition)
                    {
                        var task = Evaluate(item, context);
                        arguments.Add(task);
                        pendingTasks[arguments.Count - 1] = task;
                    }
                    else if (item.Type == ParserResultType.Number ||
                        item.Type == ParserResultType.String ||
                        item.Type == ParserResultType.Boolean)
                    {
                        arguments.Add(item.Value);
                    }
                }

                if (formula == null)
                {
                    if (pendingTasks.Count > 0)
                    {
                        await Task.WhenAll(pendingTasks.Values);

                        foreach (var item in pendingTasks)
                        {
                            arguments[item.Key] = item.Value.Result;
                        }
                    }

                    return arguments.ToArray();
                }

                var parsedArguments = (object[])((Task<object>)arguments[0]).Result;

                try
                {
                    FormulaContext formulaContext = new FormulaContext(parsedArguments)
                    {
                        Evaluator = this
                    };

                    if (formula.IsAsync)
                    {
                        return await (formula as AsyncFormula).EvaluateAsync(formulaContext);
                    }
                    else
                    {
                        return (formula as Formula).Evaluate(formulaContext);
                    }
                }
                catch(Exception ex)
                {
                    throw new EvaluationException($"Failed to evaluate '{formula.Name}' formula. {ex.Message}");
                }
            }
            else if (result is ConditionResult conditionResult)
            {
                return await Resolve(conditionResult.Value, context);
            }
            else if (result is CustomNameResult customNameResult)
            {
                return await Resolve(customNameResult.Value, context);
            }
            else
            {
                return result.Value;
            }
        }

        #region Resolver
        public async Task<bool> Resolve(Condition input, IEngineContext context = null)
        {
            var left = Evaluate(input.LeftOperand, context);
            var @operator = Evaluate(input.Operator, context);
            var right = Evaluate(input.RightOperand, context);
            await Task.WhenAll(left, @operator, right);
            return AlphaXComparer.Compare(left.Result, @operator.Result?.ToString(), right.Result, SupportedLogicalOperators);
        }

        public async Task<object> Resolve(CustomName customName, IEngineContext context = null)
        {
            if (context == null)
            {
                throw new EvaluationException($"No context found to resolve custom name ({customName.Value}).");
            }

            var resolvedValue = await context.Resolve(customName.Value);

            if (resolvedValue == null)
                return resolvedValue;

            return NormalizeValue(resolvedValue);
        }

        private static object NormalizeValue(object value)
        {
            if (value is int || value is byte)
            {
                return Convert.ToDouble(value);
            }

            if (value is Array array)
            {
                var normalized = new object[array.Length];
                for (int i = 0; i < array.Length; i++)
                    normalized[i] = NormalizeValue(array.GetValue(i));
                return normalized;
            }

            return value;
        }
        #endregion
    }
}
