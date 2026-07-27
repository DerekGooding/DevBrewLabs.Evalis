namespace DevBrewLabs.Evalis.Formulas
{
    internal class ConcatFormula : Formula
    {
        public ConcatFormula() : base("CONCAT")
        {
        }

        public override IEvaluationResult Evaluate(IFormulaContext context)
        {
            return EvaluationResult.WithValue(string.Concat(context.GetFlattenedArgs<object>()));
        }

        protected override FormulaInfo GetFormulaInfo()
        {
            FormulaInfo info = new FormulaInfo(Name)
            {
                Description = "Concatenate all the values present in the provided array."
            };
            info.AddArgument(new ObjectArgument("values", true, isVariadic: true)
            {
                Description = "Values or arrays to concatenate."
            });
            return info;
        }
    }
}