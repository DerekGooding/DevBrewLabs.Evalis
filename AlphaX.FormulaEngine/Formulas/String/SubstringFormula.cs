using System;

namespace AlphaX.FormulaEngine.Formulas
{
    public class SubstringFormula : Formula
    {
        public SubstringFormula() : base("SUBSTRING") { }

        public override object Evaluate(IFormulaContext context)
        {
            if (context.TryGetArg(0, out string text) && context.TryGetArg(1, out double startIdx))
            {
                int start = (int)startIdx;
                if (context.Args.Length > 2 && context.TryGetArg(2, out double len))
                {
                    int length = (int)len;
                    if (start < 0 || start >= text.Length || length <= 0) return string.Empty;
                    if (start + length > text.Length) length = text.Length - start;
                    return text.Substring(start, length);
                }
                if (start < 0 || start >= text.Length) return string.Empty;
                return text.Substring(start);
            }
            return string.Empty;
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