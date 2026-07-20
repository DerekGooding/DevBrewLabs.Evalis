namespace AlphaX.FormulaEngine.Formulas
{
    internal class UpperFormula : StringFormula
    {
        public UpperFormula() : base("UPPER") { }

        protected override object EvaluateString(string value)
        {
            return value?.ToUpperInvariant();
        }

        protected override FormulaInfo GetFormulaInfo()
        {
            var info = base.GetFormulaInfo();
            info.Description = "Returns the uppercase string.";
            return info;
        }
    }
}
