using AlphaX.FormulaEngine.Utils;
using System;

namespace AlphaX.FormulaEngine.Formulas
{
    internal class TextSplitFormula : Formula
    {
        public TextSplitFormula() : base("TEXTSPLIT") { }

        public override object Evaluate(params object[] args)
        {
            if (!args.TryGetArgument(0, out string separator))
            {
                throw new ArgumentException("Invalid argument at index 0. Expected a string value.");
            }

            if (!args.TryGetArgument(1, out string value))
            {
                throw new ArgumentException("Invalid argument at index 1. Expected a string value.");
            }

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
