using AlphaX.FormulaEngine.Core.Evaluation.Resolver;
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
                return this.Resolve(conditionResult.Value, context);
            }
            else if (result is CustomNameResult customNameResult)
            {
                return this.Resolve(customNameResult.Value, context);
            }
            else
            {
                return result.Value;
            }
        }
    }
}
