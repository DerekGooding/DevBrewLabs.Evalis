using AlphaX.FormulaEngine.Resources;
using AlphaX.FormulaEngine.Utils;
using System;

namespace AlphaX.FormulaEngine.Formulas
{
    internal abstract class StringFormula : Formula
    {
        protected StringFormula(string name) : base(name)
        {
        }

        public override object Evaluate(params object[] args)
        {
            if (!args.TryGetArgument(0, out string value))
            {
                throw new ArgumentException(string.Format(FormulaResources.InvalidStringArgument, 0));
            }

            return EvaluateString(value);
        }

        protected abstract object EvaluateString(string value);

        protected override FormulaInfo GetFormulaInfo()
        {
            FormulaInfo info = new FormulaInfo(Name);

            info.AddArgument(new StringArgument("value", true)
            {
                Description = "Input string value."
            });

            return info;
        }
    }
}
