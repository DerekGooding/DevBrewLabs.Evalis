namespace AlphaX.FormulaEngine.Formulas
{
    internal class SumFormula : Formula
    {
        public SumFormula() : base("SUM") { }

        public override object Evaluate(IFormulaContext context)
        {
            double sum = 0;

            for (int index = 0; index < context.Args.Length; index++)
            {
                if (context.TryGetArg(index, out double argument))
                {
                    sum += argument;
                }
            }

            return sum;
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
