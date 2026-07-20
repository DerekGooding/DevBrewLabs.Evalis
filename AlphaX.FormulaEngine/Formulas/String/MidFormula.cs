using System;

namespace AlphaX.FormulaEngine.Formulas
{
    /// <summary>
    /// Represents the MID formula which returns a specific number of characters from a text string, starting at the position you specify (1-indexed).
    /// </summary>
    internal class MidFormula : Formula
    {
        /// <summary>
        /// Initializes a new MID formula.
        /// </summary>
        public MidFormula() : base("MID") { }

        /// <inheritdoc/>
        public override object Evaluate(IFormulaContext context)
        {
            ValidateArgumentCount(context.Args);
            string text = context.GetStringArg(0);
            
            if (string.IsNullOrEmpty(text))
                return text;

            int start = (int)context.GetDoubleArg(1);
            int count = (int)context.GetDoubleArg(2);

            if (start < 1)
                throw new EvaluationException("Start position in MID must be greater than or equal to 1.");
            if (count < 0)
                throw new EvaluationException("Count in MID must be greater than or equal to 0.");

            int startIndex = start - 1; // 1-indexed to 0-indexed

            if (startIndex >= text.Length)
                return string.Empty;

            int charsToTake = Math.Min(count, text.Length - startIndex);
            return text.Substring(startIndex, charsToTake);
        }

        /// <inheritdoc/>
        protected override FormulaInfo GetFormulaInfo()
        {
            FormulaInfo info = new FormulaInfo(Name)
            {
                Description = "Returns a specific number of characters from a text string, starting at the position you specify (1-indexed)."
            };
            info.AddArgument(new StringArgument("text", true) { Description = "The text string." });
            info.AddArgument(new DoubleArgument("start_num", true) { Description = "The position of the first character you want to extract (1-indexed)." });
            info.AddArgument(new DoubleArgument("num_chars", true) { Description = "The number of characters to extract." });
            return info;
        }
    }
}
