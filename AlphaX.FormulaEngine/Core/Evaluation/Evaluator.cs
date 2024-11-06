using AlphaX.FormulaEngine.Core.Evaluation.Resolver;
using AlphaX.Parserz;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace AlphaX.FormulaEngine
{
    internal class Evaluator : IEvaluator
    {
        private AlphaXFormulaEngine _engine;
        private ConditionResolver _conditionResolver;
        private CustomNameResolver _customNameResolver;

        public Evaluator(AlphaXFormulaEngine engine)
        {
            _engine = engine;
            _conditionResolver = new ConditionResolver(engine);
            _customNameResolver = new CustomNameResolver(engine);
        }

        public object Evaluate(IParserResult result)
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
                        if (!_engine.FormulaStore.Contains(formulaName))
                            throw new EvaluationException($"Invalid formula '{formulaName}'");

                        formula = _engine.FormulaStore.Get(formulaName);
                        continue;
                    }
                    
                    if (item.Type == ParserResultType.Array 
                        || item.Type == FormulaParserResultType.CustomName 
                        || item.Type == FormulaParserResultType.Condition)
                    {
                        arguments.Add(Evaluate(item));
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
                formula.ValidateArguments(parsedArguments);

                try
                {
                    return formula.Evaluate(parsedArguments);
                }
                catch
                {
                    throw new EvaluationException($"Failed to evaluate '{formula.Name}' formula.");
                }
            }
            else if (result is ConditionResult conditionResult)
            {
                return _conditionResolver.Resolve(conditionResult.Value);
            }
            else if (result is CustomNameResult customNameResult)
            {
                return _customNameResolver.Resolve(customNameResult.Value);
            }
            else
            {
                return result.Value;
            }
        }
    }
}
