using System;
using System.Globalization;

namespace DevBrewLabs.Evalis.Formulas
{
    internal class DateTimeFormula : Formula
    {
        public DateTimeFormula() : base("DATETIME")
        {
        }

        public override IEvaluationResult Evaluate(IFormulaContext context)
        {
            string value = context.GetStringArg(0);
            context.TryGetArg(1, out string format);

            if (string.IsNullOrEmpty(format))
            {
                format = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;
            }

            return EvaluationResult.WithValue(DateTime.ParseExact(value, format, CultureInfo.CurrentCulture));
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