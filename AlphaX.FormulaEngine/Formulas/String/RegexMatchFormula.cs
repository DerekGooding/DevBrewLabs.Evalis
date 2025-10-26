using AlphaX.FormulaEngine.Utils;
using System;
using System.Text.RegularExpressions;

namespace AlphaX.FormulaEngine.Formulas
{
    internal class RegexMatchFormula : Formula
    {
        public RegexMatchFormula() : base("REGEXMATCH")
        {
            
        }

        public override object Evaluate(params object[] args)
        {
            if (!args.TryGetArgument(0, out string pattern))
            {
                throw new ArgumentException("Invalid argument at index 0. Expected a string value.");
            }

            if (!args.TryGetArgument(1, out string value))
            {
                throw new ArgumentException("Invalid argument at index 1. Expected a string value.");
            }

            Regex regex = new Regex(pattern);
            return regex.IsMatch(value);
        }

        protected override FormulaInfo GetFormulaInfo()
        {
            FormulaInfo info = new FormulaInfo(Name)
            {
                Description = "Searches the input string for the first occurence of regular expression."
            };

            info.AddArgument(new StringArgument("pattern", true)
            {
                Description = "Pattern to match."
            });

            info.AddArgument(new StringArgument("value", true)
            {
                Description = "Input value."
            });
            return info;
        }
    }
}
