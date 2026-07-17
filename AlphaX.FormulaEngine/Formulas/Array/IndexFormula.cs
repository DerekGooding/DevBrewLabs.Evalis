using System;
using System.Collections;
using System.Linq;

namespace AlphaX.FormulaEngine.Formulas
{
    public class IndexFormula : Formula
    {
        public IndexFormula() : base("INDEX") { }

        public override object Evaluate(IFormulaContext context)
        {
            if (context.Args.Length > 1 && context.Args[0] is IEnumerable enumerable && !(context.Args[0] is string))
            {
                if (context.TryGetArg(1, out double index))
                {
                    var array = enumerable.Cast<object>().ToArray();
                    int idx = (int)index;
                    if (idx >= 0 && idx < array.Length)
                    {
                        return array[idx];
                    }
                    throw new IndexOutOfRangeException("Index is out of range.");
                }
            }
            throw new ArgumentException("Invalid arguments for INDEX.");
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