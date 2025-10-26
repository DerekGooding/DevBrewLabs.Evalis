namespace AlphaX.FormulaEngine.Formulas
{
    internal class LowerFormula : StringFormula
    {
        public LowerFormula() : base("LOWER") { }

        protected override object EvaluateString(string value)
        {
            return value?.ToLowerInvariant();
        }

        protected override FormulaInfo GetFormulaInfo()
        {
            var info = base.GetFormulaInfo();
            info.Description = "Returns the lowercase string.";
            return info;
        }
    }
}