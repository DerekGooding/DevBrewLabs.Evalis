using System;
using System.Collections;
using System.Linq;

namespace AlphaX.FormulaEngine.Formulas
{
    public class JoinFormula : Formula
    {
        public JoinFormula() : base("JOIN") { }

        public override object Evaluate(IFormulaContext context)
        {
            if (context.Args.Length > 1 && context.TryGetArg(0, out string separator))
            {
                if (context.Args[1] is IEnumerable enumerable && !(context.Args[1] is string))
                {
                    var array = enumerable.Cast<object>().Select(x => x?.ToString() ?? "").ToArray();
                    return string.Join(separator, array);
                }
            }
            throw new ArgumentException("Invalid arguments for JOIN.");
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