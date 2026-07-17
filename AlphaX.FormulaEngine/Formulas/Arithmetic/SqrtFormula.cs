using System;

namespace AlphaX.FormulaEngine.Formulas
{
    public class SqrtFormula : Formula
    {
        public SqrtFormula() : base("SQRT") { }

        public override object Evaluate(IFormulaContext context)
        {
            if (context.TryGetArg(0, out double num))
            {
                return Math.Sqrt(num);
            }
            return 0d;
        }

        protected override FormulaInfo GetFormulaInfo()
        {
            FormulaInfo info = new FormulaInfo(Name) { Description = "Returns square root." };
            info.AddArgument(new DoubleArgument("number", true));
            return info;
        }
    }
}