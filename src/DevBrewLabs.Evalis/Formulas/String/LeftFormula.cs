using System;

namespace DevBrewLabs.Evalis.Formulas
{
    /// <summary>
    /// Represents the LEFT formula which returns the first character or characters in a text string, based on the number of characters you specify.
    /// </summary>
    internal class LeftFormula : Formula
    {
        /// <summary>
        /// Initializes a new LEFT formula.
        /// </summary>
        public LeftFormula() : base("LEFT") { }

        /// <inheritdoc/>
        public override IEvaluationResult Evaluate(IFormulaContext context)
        {
            
            string text = context.GetStringArg(0);
            
            if (string.IsNullOrEmpty(text))
                return EvaluationResult.WithValue(text);

            double count = 1;
            if (context.TryGetArg(1, out double argCount))
                count = argCount;

            int charsToTake = Math.Max(0, (int)count);
            return EvaluationResult.WithValue(text.Substring(0, Math.Min(charsToTake, text.Length)));
        }

        /// <inheritdoc/>
        protected override FormulaInfo GetFormulaInfo()
        {
            FormulaInfo info = new FormulaInfo(Name)
            {
                Description = "Returns the specified number of characters from the start of a text string."
            };
            info.AddArgument(new StringArgument("text", true) { Description = "The text string." });
            info.AddArgument(new DoubleArgument("count", false) { Description = "Optional. The number of characters to extract (default is 1)." });
            return info;
        }
    }
}
