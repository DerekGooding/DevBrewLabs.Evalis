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

        public override object Evaluate(IFormulaContext context)
        {
			string pattern = context.GetStringArg(0);
			string value = context.GetStringArg(1);
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
