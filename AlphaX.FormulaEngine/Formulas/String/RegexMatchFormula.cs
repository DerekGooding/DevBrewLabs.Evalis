using AlphaX.FormulaEngine.Resources;
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
                throw new ArgumentException(string.Format(FormulaResources.InvalidStringArgument, 0));
            }

            if (!args.TryGetArgument(1, out string value))
            {
                throw new ArgumentException(string.Format(FormulaResources.InvalidStringArgument, 1));
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
