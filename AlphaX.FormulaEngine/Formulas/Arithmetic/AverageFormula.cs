namespace AlphaX.FormulaEngine.Formulas
{
    internal class AverageFormula : Formula
    {
        public AverageFormula() : base("AVERAGE") { }

        public override IEvaluationResult Evaluate(IFormulaContext context)
        {
            double sum = 0;
            int count = 0;

            foreach (double value in context.GetFlattenedArgs<double>())
            {
                sum += value;
                count++;
            }

            return EvaluationResult.WithValue(count == 0 ? 0d : sum / count);
        }

        protected override FormulaInfo GetFormulaInfo()
        {
            FormulaInfo info = new FormulaInfo(Name)
            {
                Description = "Returns average of provided values."
            };
            info.AddArgument(new ArrayArgument("values", true)
            {
                Description = "Array of numeric values."
            });
            return info;
        }
    }
}
