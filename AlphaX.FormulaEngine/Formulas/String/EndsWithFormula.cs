using System;

namespace AlphaX.FormulaEngine.Formulas
{
    internal class EndsWithFormula : Formula
    {
        public EndsWithFormula() : base("ENDSWITH") { }

        public override IEvaluationResult Evaluate(IFormulaContext context)
        {
            ValidateArgumentCount(context.Args);

			string source = context.GetStringArg(0);
			string value = context.GetStringArg(1);

			context.TryGetArg(2, out bool matchCase);
            return EvaluationResult.WithValue(source.EndsWith(value, matchCase ? StringComparison.Ordinal : StringComparison.InvariantCultureIgnoreCase));
        }

        protected override FormulaInfo GetFormulaInfo()
        {
            FormulaInfo info = new FormulaInfo(Name)
            {
                Description = "Checks if the provided string ends with the speicifed value."
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
