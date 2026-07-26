namespace DevBrewLabs.Evalis.Formulas
{
    internal class IfFormula : Formula
    {
        public IfFormula() : base("IF")
        {

        }

        public override IEvaluationResult Evaluate(IFormulaContext context)
        {
            bool condition = context.GetBooleanArg(0);
            object trueValue = context.GetObjectArg(1);
            object falseValue = context.GetObjectArg(2);
            return EvaluationResult.WithValue(condition ? trueValue : falseValue);
        }

        protected override FormulaInfo GetFormulaInfo()
        {
            FormulaInfo info = new FormulaInfo(Name)
            {
                Description = "Checks whether condition is met. Returns first value if true and return second value if false."
            };
            info.AddArgument(new BooleanArgument("condition", true)
            {
                Description = "Condition to evaluate."
            });
            info.AddArgument(new ObjectArgument("value1", true)
            {
                Description = "Value to return if condition is true."
            });
            info.AddArgument(new ObjectArgument("value2", true)
            {
                Description = "Value to return if condition is false."
            });
            return info;
        }
    }
}
