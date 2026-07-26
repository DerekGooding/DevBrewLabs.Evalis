using System.Text.RegularExpressions;

namespace DevBrewLabs.Evalis.Formulas
{
    internal class ReplaceFormula : Formula
    {
        public ReplaceFormula() : base("REPLACE") { }

        public override IEvaluationResult Evaluate(IFormulaContext context)
        {
            

            string source = context.GetStringArg(0);
            string oldValue = context.GetStringArg(1);
            string newValue = context.GetStringArg(2);

            context.TryGetArg(3, out bool replaceAll);

            if (replaceAll)
            {
                return EvaluationResult.WithValue(Regex.Replace(source, oldValue, newValue));
            }
            else
            {
                Regex regex = new Regex(oldValue);
                return EvaluationResult.WithValue(regex.Replace(source, newValue, 1));
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
