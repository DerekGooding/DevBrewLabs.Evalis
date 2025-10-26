using AlphaX.Parserz;
using System;
using System.Collections.Generic;

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

        public object Evaluate(IParserResult result, IEngineContext context)
        {
            if(result is ArrayResult nodes)
            {
                List<object> arguments = new List<object>();

                Formula formula = null;

                for (int index = 0; index < nodes.Value.Length; index++)
                {
                    var item = nodes.Value[index];

                    if (item.Type == FormulaParserResultType.FormulaName)
                    {
                        var formulaName = item.Value.ToString();

                        if (!_formulaStore.Contains(formulaName))
                            throw new EvaluationException($"Invalid formula '{formulaName}'");

                        formula = _formulaStore.Get(formulaName);
                        continue;
                    }

                    if (item.Type == ParserResultType.Array 
                        || item.Type == FormulaParserResultType.CustomName 
                        || item.Type == FormulaParserResultType.Condition)
                    {
                        arguments.Add(Evaluate(item, context));
                    }
                    else if (item.Type == ParserResultType.Number ||
                        item.Type == ParserResultType.String ||
                        item.Type == ParserResultType.Boolean)
                    {
                        arguments.Add(item.Value);
                    }
                }

                if (formula == null)
                    return arguments.ToArray();

                var parsedArguments = (object[])arguments[0];

                try
                {
                    FormulaContext formulaContext = new FormulaContext(parsedArguments)
                    {
                        Evaluator = this
                    };

                    return formula.Evaluate(formulaContext);
                }
                catch(Exception ex)
                {
                    throw new EvaluationException($"Failed to evaluate '{formula.Name}' formula. {ex.Message}");
                }
            }
            else if (result is ConditionResult conditionResult)
            {
                return Resolve(conditionResult.Value, context);
            }
            else if (result is CustomNameResult customNameResult)
            {
                return Resolve(customNameResult.Value, context);
            }
            else
            {
                return result.Value;
            }
        }

        #region Resolver
        public bool Resolve(Condition input, IEngineContext context = null)
        {
            var left = Evaluate(input.LeftOperand, context);
            var @operator = Evaluate(input.Operator, context);
            var right = Evaluate(input.RightOperand, context);
            return AlphaXComparer.Compare(left, @operator?.ToString(), right, SupportedLogicalOperators);
        }

        public object Resolve(CustomName customName, IEngineContext context = null)
        {
            if (context == null)
            {
                throw new EvaluationException($"No context found to resolve custom name ({customName.Value}).");
            }

            var resolvedValue = context.Resolve(customName.Value);

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
