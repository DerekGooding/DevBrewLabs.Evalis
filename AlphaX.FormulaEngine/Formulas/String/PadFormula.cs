using System;

namespace AlphaX.FormulaEngine.Formulas
{
    /// <summary>
    /// Represents the PAD formula which pads a string to a specific length.
    /// </summary>
    internal class PadFormula : Formula
    {
        /// <summary>
        /// Initializes a new PAD formula.
        /// </summary>
        public PadFormula() : base("PAD") { }

        /// <inheritdoc/>
        public override object Evaluate(IFormulaContext context)
        {
            ValidateArgumentCount(context.Args);
            string text = context.GetStringArg(0);
            int totalWidth = (int)context.GetDoubleArg(1);
            
            char padChar = ' ';
            if (context.TryGetArg(2, out string pChar) && !string.IsNullOrEmpty(pChar))
            {
                padChar = pChar[0];
            }

            bool rightPad = true; // "right" padding means text is on the left, padding on the right (PadRight)
            if (context.TryGetArg(3, out string direction))
            {
                if (direction.Equals("left", StringComparison.OrdinalIgnoreCase))
                    rightPad = false; // "left" padding means text is on the right, padding on the left (PadLeft)
            }

            if (text == null) text = string.Empty;

            if (rightPad)
                return text.PadRight(totalWidth, padChar);
            else
                return text.PadLeft(totalWidth, padChar);
        }

        /// <inheritdoc/>
        protected override FormulaInfo GetFormulaInfo()
        {
            FormulaInfo info = new FormulaInfo(Name)
            {
                Description = "Pads a string to a specified length."
            };
            info.AddArgument(new StringArgument("text", true) { Description = "The text to pad." });
            info.AddArgument(new DoubleArgument("length", true) { Description = "The total width to pad to." });
            info.AddArgument(new StringArgument("pad_char", false) { Description = "Optional. The character to pad with (default is space)." });
            info.AddArgument(new StringArgument("direction", false) { Description = "Optional. 'left' or 'right' (default is 'right')." });
            return info;
        }
    }
}
