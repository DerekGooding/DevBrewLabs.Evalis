namespace AlphaX.FormulaEngine.Formulas
{
    internal class LengthFormula : StringFormula
    {
        public LengthFormula() : base("LENGTH")
        {
        }

        protected override object EvaluateString(string value)
        {
            return value?.Length;
        }

        protected override FormulaInfo GetFormulaInfo()
        {
            var info = base.GetFormulaInfo();
            info.Description = "Returns the length of string.";
            return info;
        }
    }
}