using AlphaX.FormulaEngine.Utils;
using System;
using System.Globalization;

namespace AlphaX.FormulaEngine.Formulas
{
    internal class DateTimeFormula : Formula
    {
        public DateTimeFormula() : base("DATETIME")
        {

        }

        public override object Evaluate(params object[] args)
        {
            string value = args.GetValueOrDefault(0, string.Empty);
            string format = args.GetValueOrDefault(1, string.Empty);

            if (!string.IsNullOrEmpty(format))
            {
                return DateTime.ParseExact(value, format, CultureInfo.CurrentCulture);            
            }

            return DateTime.Parse(value);
        }

        protected override FormulaInfo GetFormulaInfo()
        {
            FormulaInfo info = new FormulaInfo(Name)
            {
                Description = "Convert a valid date string to date object."
            };
            info.AddArgument(new StringArgument("value", true)
            {
                Description = "Date string to convert."
            });
            info.AddArgument(new StringArgument("format", false)
            {
                Description = "Exact date format to use for conversion."
            });
            return info;
        }
    }
}
