namespace AlphaX.FormulaEngine.Formulas
{
    internal class SumFormula : Formula
    {
        public SumFormula() : base("SUM") { }

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
            info.AddArgument(new ArrayArgument("values", true)
            {
                Description = "Array of numeric values"
            });
            return info;
        }
    }
}
