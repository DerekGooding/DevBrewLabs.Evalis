namespace AlphaX.FormulaEngine.Formulas
{
    internal class AverageFormula : Formula
    {
        public AverageFormula() : base("AVERAGE") { }

        public override object Evaluate(IFormulaContext context)
        {
            double sum = 0;
            int totalArguments = 0;

            for (int index = 0; index < context.Args.Length; index++)
            {
                if(context.TryGetArg(index, out double argument))
                {
                    sum += argument;
                    totalArguments++;
                }
            }

            return sum / totalArguments;
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
