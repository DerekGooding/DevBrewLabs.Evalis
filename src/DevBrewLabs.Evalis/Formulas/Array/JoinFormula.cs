using System.Collections;
using System.Linq;

namespace DevBrewLabs.Evalis.Formulas
{
    public class JoinFormula : Formula
    {
        public JoinFormula() : base("JOIN")
        {
        }

        public override IEvaluationResult Evaluate(IFormulaContext context)
        {
            if (context.Args.Length > 1 && context.TryGetArg(0, out string separator))
            {
                if (context.Args[1] is IEnumerable enumerable && !(context.Args[1] is string))
                {
                    var array = enumerable.Cast<object>().Select(x => x?.ToString() ?? "").ToArray();
                    return EvaluationResult.WithValue(string.Join(separator, array));
                }
            }
            return EvaluationResult.WithError(Error.Value("Invalid arguments for JOIN."));
        }

        protected override FormulaInfo GetFormulaInfo()
        {
            FormulaInfo info = new FormulaInfo(Name) { Description = "Joins array into string." };
            info.AddArgument(new StringArgument("separator", true));
            info.AddArgument(new ArrayArgument("array", true));
            return info;
        }
    }
}