using System.Collections;
using System.Linq;

namespace DevBrewLabs.Evalis.Formulas
{
    public class IndexFormula : Formula
    {
        public IndexFormula() : base("INDEX")
        {
        }

        public override IEvaluationResult Evaluate(IFormulaContext context)
        {
            if (context.Args.Length > 1 && context.Args[0] is IEnumerable enumerable && !(context.Args[0] is string))
            {
                if (context.TryGetArg(1, out double index))
                {
                    var array = enumerable.Cast<object>().ToArray();
                    int idx = (int)index;
                    return idx >= 0 && idx < array.Length
                        ? EvaluationResult.WithValue(array[idx])
                        : EvaluationResult.WithError(Error.Value("Index is out of range."));
                }
            }

            return EvaluationResult.WithError(Error.Value("Invalid arguments for INDEX."));
        }

        protected override FormulaInfo GetFormulaInfo()
        {
            FormulaInfo info = new FormulaInfo(Name) { Description = "Returns value at index." };
            info.AddArgument(new ArrayArgument("array", true));
            info.AddArgument(new DoubleArgument("index", true));
            return info;
        }
    }
}