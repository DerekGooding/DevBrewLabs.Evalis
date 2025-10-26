using AlphaX.FormulaEngine.Resources;
using AlphaX.FormulaEngine.Utils;
using System;

namespace AlphaX.FormulaEngine.Formulas
{
    internal class TextSplitFormula : Formula
    {
        public TextSplitFormula() : base("TEXTSPLIT") { }

        public override object Evaluate(IFormulaContext context)
        {
			string separator = context.GetStringArg(0);
			string value = context.GetStringArg(1);

			return value.Split(new string[] { separator }, StringSplitOptions.None);
        }

        protected override FormulaInfo GetFormulaInfo()
        {
           FormulaInfo info = new FormulaInfo(Name)
            {
                Description = "Splits the input string into an array using the provided delimiter."
            };

            info.AddArgument(new StringArgument("separator", true)
            {
                Description = "The delimiter to use for string split."
            });

            info.AddArgument(new StringArgument("value", true)
            {
                Description = "The input string."
            });

            return info;
        }
    }
}
