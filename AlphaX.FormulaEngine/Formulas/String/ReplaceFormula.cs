using AlphaX.FormulaEngine.Utils;
using System;
using System.Text.RegularExpressions;

namespace AlphaX.FormulaEngine.Formulas
{
    internal class ReplaceFormula : Formula
    {
        public ReplaceFormula() : base("REPLACE") { }

        public override object Evaluate(params object[] args)
        {
            ValidateArgumentCount(args);

            if (!args.TryGetArgument(0, out string source))
            {
                throw new ArgumentException("Invalid argument at index 0. Expected a string value.");
            }

            if (!args.TryGetArgument(1, out string oldValue))
            {
                throw new ArgumentException("Invalid argument at index 1. Expected a string value.");
            }

            if (!args.TryGetArgument(2, out string newValue))
            {
                throw new ArgumentException("Invalid argument at index 2. Expected a string value.");
            }

            args.TryGetArgument(3, out bool replaceAll);

            if (replaceAll)
            {
                return Regex.Replace(source, oldValue, newValue);
            }
            else
            {
                Regex regex = new Regex(oldValue);
                return regex.Replace(source, newValue, 1);
            }
        }

        protected override FormulaInfo GetFormulaInfo()
        {
            FormulaInfo info = new FormulaInfo(Name)
            {
                Description = "Returns a new string in which all occurences of the specified string are replaced by another string."
            };

            info.AddArgument(new StringArgument("source", true)
            {
                Description = "The source string."
            });

            info.AddArgument(new StringArgument("oldValue", true)
            {
                Description = "The string to be replaced."
            });

            info.AddArgument(new StringArgument("newValue", true)
            {
                Description = "The string to replace with the old value."
            });

            info.AddArgument(new BooleanArgument("replaceAll", false)
            {
                Description = "True if replace all occurences. False if replace only the first occurence. Default = true"
            });

            return info;
        }
    }
}