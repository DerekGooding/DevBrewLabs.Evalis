using System;

namespace AlphaX.FormulaEngine.Formulas
{
    internal class OperatorFormula : Formula
    {
        protected Func<string> _getOperator;

        internal OperatorFormula(string name, Func<string> getOperator) : base(name)
        {
            _getOperator = getOperator;
        }

        public override IEvaluationResult Evaluate(IFormulaContext context)
        {
            return EvaluationResult.WithValue(AlphaXUtil.Compare(context.Args[0], _getOperator(), context.Args[1], (context as FormulaContext).Evaluator.SupportedLogicalOperators));
        }

        protected override FormulaInfo GetFormulaInfo()
        {
            FormulaInfo info = new FormulaInfo(Name)
            {
                Description = $"Performs '{Name}' logical operation between two operands."
            };
            info.AddArgument(new ObjectArgument("value1", true)
            {
                Description = "The first operand."
            });
            info.AddArgument(new ObjectArgument("value2", true)
            {
                Description = "The second operand."
            });
            return info;
        }
    }
}
