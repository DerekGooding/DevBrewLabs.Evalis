using AlphaX.FormulaEngine.Utils;

namespace AlphaX.FormulaEngine.Formulas
{
    internal abstract class StringFormula : Formula
    {
        protected StringFormula(string name) : base(name)
        {
        }

        public override object Evaluate(params object[] args)
        {
            return EvaluateString(args.GetValueOrDefault(0, string.Empty));
        }

        protected abstract object EvaluateString(string value);
    }
}
