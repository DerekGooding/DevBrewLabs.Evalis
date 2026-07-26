namespace DevBrewLabs.Evalis.Formulas
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
            info.AddArgument(new ObjectArgument("source", false, isVariadic: true)
            {
                Description = "Input array elements."
            });
            return info;
        }
    }
}
