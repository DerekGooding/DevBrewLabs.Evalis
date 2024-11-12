using System;
using AlphaX.Parserz;
using System.Collections.Generic;

namespace AlphaX.FormulaEngine.Core.Evaluation.Resolver
{
    internal static class ResolverExtensions
    {
        public static bool Resolve(this Evaluator evaluator, Condition input, IEngineContext context = null)
        {
            var left = evaluator.Evaluate(input.LeftOperand, context);
            var @operator = evaluator.Evaluate(input.Operator, context);
            var right = evaluator.Evaluate(input.RightOperand, context);
            return AlphaXComparer.Compare(left, @operator?.ToString(), right, evaluator.SupportedLogicalOperators);
        }

        public static object Resolve(this Evaluator evaluator, CustomName customName, IEngineContext context = null)
        {
            if (context == null)
            {
                throw new EvaluationException($"No context found to resolve custom name ({customName.Value}).");
            }

            var resolvedValue = context.Resolve(customName.Value);

            if (resolvedValue == null)
                return resolvedValue;

            if (resolvedValue is int || resolvedValue is byte)
            {
                resolvedValue = Convert.ToDouble(resolvedValue);
            }
            else if (resolvedValue is Array array)
            {
                object[] objArray = new object[array.Length];

                for (int index = 0; index < array.Length; index++)
                {
                    var arrayItem = array.GetValue(index);

                    if (arrayItem is int || arrayItem is byte)
                    {
                        objArray[index] = Convert.ToDouble(arrayItem);
                    }
                    else
                    {
                        objArray[index] = arrayItem;
                    }
                }

                resolvedValue = objArray;
            }

            return resolvedValue;
        }
    }
}
