namespace DevBrewLabs.Evalis.Formulas
{
    internal class SumFormula : Formula
    {
        public SumFormula() : base("SUM")
        {
        }

        public override IEvaluationResult Evaluate(IFormulaContext context)
        {
            double sum = 0;

            foreach (double value in context.GetFlattenedArgs<double>())
                sum += value;

            return EvaluationResult.WithValue(sum);
        }

        protected override FormulaInfo GetFormulaInfo()
        {
            FormulaInfo info = new FormulaInfo(Name)
            {
                Description = "Returns sum of provided values"
            };
            info.AddArgument(new DoubleArgument("values", true, isVariadic: true)
            {
                Description = "Numeric values or arrays to sum"
            });
            return info;
        }
    }
}