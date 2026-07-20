using System;
using System.Globalization;

namespace AlphaX.FormulaEngine.Formulas
{
    /// <summary>
    /// Represents the PROPER formula which capitalizes the first letter in each word of a text value.
    /// </summary>
    internal class ProperFormula : Formula
    {
        /// <summary>
        /// Initializes a new PROPER formula.
        /// </summary>
        public ProperFormula() : base("PROPER") { }

        /// <inheritdoc/>
        public override object Evaluate(IFormulaContext context)
        {
            ValidateArgumentCount(context.Args);
            string text = context.GetStringArg(0);

            if (string.IsNullOrEmpty(text))
                return text;

            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(text.ToLower());
        }

        /// <inheritdoc/>
        protected override FormulaInfo GetFormulaInfo()
        {
            FormulaInfo info = new FormulaInfo(Name)
            {
                Description = "Capitalizes the first letter in each word of a text value."
            };
            info.AddArgument(new StringArgument("text", true) { Description = "The text to convert to proper case." });
            return info;
        }
    }
}
