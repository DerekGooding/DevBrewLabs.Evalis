namespace AlphaX.FormulaEngine.Formulas
{
    public class ArrayFormula : Formula
    {
        public ArrayFormula() : base("ARRAY")
        {
        }

        public override IEvaluationResult Evaluate(IFormulaContext context)
        {
            return EvaluationResult.WithValue(context.Args);
        }

        protected override FormulaInfo GetFormulaInfo()
        {
            FormulaInfo info = new FormulaInfo(Name)
            {
                Description = "Returns an array of values."
            };
            info.AddArgument(new ArrayArgument("source", true)
            {
                Description = "Input array."
            });
            return info;
        }
    }
}
