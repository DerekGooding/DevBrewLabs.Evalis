namespace DevBrewLabs.Evalis.Formulas
{
    public class SubstringFormula : Formula
    {
        public SubstringFormula() : base("SUBSTRING")
        {
        }

        public override IEvaluationResult Evaluate(IFormulaContext context)
        {
            if (context.TryGetArg(0, out string text) && context.TryGetArg(1, out double startIdx))
            {
                int start = (int)startIdx;
                if (context.Args.Length > 2 && context.TryGetArg(2, out double len))
                {
                    int length = (int)len;
                    if (start < 0 || start >= text.Length || length <= 0) return EvaluationResult.WithValue(string.Empty);
                    if (start + length > text.Length) length = text.Length - start;
                    return EvaluationResult.WithValue(text.Substring(start, length));
                }
                if (start < 0 || start >= text.Length) return EvaluationResult.WithValue(string.Empty);
                return EvaluationResult.WithValue(text.Substring(start));
            }
            return EvaluationResult.WithValue(string.Empty);
        }

        protected override FormulaInfo GetFormulaInfo()
        {
            FormulaInfo info = new FormulaInfo(Name) { Description = "Extracts substring." };
            info.AddArgument(new StringArgument("text", true));
            info.AddArgument(new DoubleArgument("startIndex", true));
            info.AddArgument(new DoubleArgument("length", false));
            return info;
        }
    }
}