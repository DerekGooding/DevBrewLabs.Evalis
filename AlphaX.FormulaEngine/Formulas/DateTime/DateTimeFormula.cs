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
            if (args.Length == 2)
            {
                return DateTime.ParseExact((string)args[0], (string)args[1], CultureInfo.CurrentCulture);            
            }

            return DateTime.Parse((string)args[0]);
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
