using System.Collections;
using System.Linq;

namespace DevBrewLabs.Evalis.Formulas
{
    public class CountFormula : Formula
    {
        public CountFormula() : base("COUNT")
        {
        }

        public override IEvaluationResult Evaluate(IFormulaContext context)
        {
            if (context.Args.Length > 0 && context.Args[0] is IEnumerable enumerable && !(context.Args[0] is string))
            {
                return EvaluationResult.WithValue((double)enumerable.Cast<object>().Count());
            }
            return EvaluationResult.WithValue(1d);
        }

        protected override FormulaInfo GetFormulaInfo()
        {
            FormulaInfo info = new FormulaInfo(Name) { Description = "Counts items in array." };
            info.AddArgument(new ArrayArgument("array", true));
            return info;
        }
    }
}