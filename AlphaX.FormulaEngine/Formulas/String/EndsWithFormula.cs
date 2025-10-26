using AlphaX.FormulaEngine.Resources;
using AlphaX.FormulaEngine.Utils;
using System;

namespace AlphaX.FormulaEngine.Formulas
{
    internal class EndsWithFormula : Formula
    {
        public EndsWithFormula() : base("ENDSWITH") { }

        public override object Evaluate(params object[] args)
        {
            ValidateArgumentCount(args);

            if (!args.TryGetArgument(0, out string source))
            {
                throw new ArgumentException(string.Format(FormulaResources.InvalidStringArgument, 0));
            }

            if (!args.TryGetArgument(1, out string value))
            {
                throw new ArgumentException(string.Format(FormulaResources.InvalidStringArgument, 1));
            }

            args.TryGetArgument(2, out bool matchCase);
            return source.EndsWith(value, matchCase ? System.StringComparison.Ordinal : System.StringComparison.InvariantCultureIgnoreCase);
        }

        protected override FormulaInfo GetFormulaInfo()
        {
            FormulaInfo info = new FormulaInfo(Name)
            {
                Description = "Checks if the provided string ends with the speicifed value."
            };
            info.AddArgument(new StringArgument("source", true)
            {
                Description = "The source string."
            });
            info.AddArgument(new StringArgument("value", true)
            {
                Description = "The value to check for."
            });
            info.AddArgument(new BooleanArgument("matchCase", false)
            {
                Description = "Match case while checking."
            });
            return info;
        }
    }
}
