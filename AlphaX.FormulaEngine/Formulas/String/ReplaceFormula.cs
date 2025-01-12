using AlphaX.FormulaEngine.Utils;
using System.Text.RegularExpressions;

namespace AlphaX.FormulaEngine.Formulas
{
    internal class ReplaceFormula : Formula
    {
        public ReplaceFormula() : base("REPLACE") { }

        public override object Evaluate(params object[] args)
        {
            var source = args.GetValueOrDefault(0, string.Empty);
            var oldValue = args.GetValueOrDefault(1, string.Empty);
            var newValue = args.GetValueOrDefault(2, string.Empty);
            var replaceAll = args.GetValueOrDefault(3, true);
           
            if (replaceAll)
            {
                return Regex.Replace(source, oldValue, newValue);
            }
            else
            {
                Regex regex = new Regex(oldValue);
                return regex.Replace(source, newValue, 1);
            }
        }

        protected override FormulaInfo GetFormulaInfo()
        {
            FormulaInfo info = new FormulaInfo(Name)
            {
                Description = "Returns a new string in which all occurences of the specified string are replaced by another string."
            };

            info.AddArgument(new StringArgument("source", true)
            {
                Description = "The source string."
            });

            info.AddArgument(new StringArgument("oldValue", true)
            {
                Description = "The string to be replaced."
            });

            info.AddArgument(new StringArgument("newValue", true)
            {
                Description = "The string to replace with the old value."
            });

            info.AddArgument(new BooleanArgument("replaceAll", false)
            {
                Description = "True if replace all occurences. False if replace only the first occurence. Default = true"
            });

            return info;
        }
    }
}