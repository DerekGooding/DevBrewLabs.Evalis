using AlphaX.FormulaEngine.Utils;

namespace AlphaX.FormulaEngine.Formulas
{
    internal class StartsWithFormula : Formula
    {
        public StartsWithFormula() : base("STARTSWITH") { }

        public override object Evaluate(params object[] args)
        {
            string source = args.GetValueOrDefault(0, string.Empty);
            string value = args.GetValueOrDefault(1, string.Empty);
            var matchCase = args.GetValueOrDefault(2, false);
            return source.StartsWith(value, matchCase ? System.StringComparison.Ordinal : System.StringComparison.InvariantCultureIgnoreCase);
        }

        protected override FormulaInfo GetFormulaInfo()
        {
            FormulaInfo info = new FormulaInfo(Name)
            {
                Description = "Checks if the provided string starts with the speicifed value."
            };
            info.AddArgument(new StringArgument("source", true)
            {
                Description = "The source string."
            });
            info.AddArgument(new StringArgument("value", true)
            {
                Description = "The value to check for."
            });
            info.AddArgument(new BooleanArgument("matchCase", false)
            {
                Description = "Match case while checking."
            });
            return info;
        }
    }
}
