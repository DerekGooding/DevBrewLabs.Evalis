using System;

namespace AlphaX.FormulaEngine.Core.Evaluation.Resolver
{
    internal class CustomNameResolver : ArgumentResolver<CustomName, object>
    {
        public CustomNameResolver(AlphaXFormulaEngine engine) : base(engine)
        {
        }

        public override object Resolve(CustomName customName)
        {
            if (Engine.Context == null)
            {
                throw new EvaluationException($"No context found to resolve custom name ({customName.Value}).");
            }

            var resolvedValue = Engine.Context.Resolve(customName.Value);

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
