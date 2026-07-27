using System;

namespace DevBrewLabs.Evalis.Formulas
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
            object leftVal = context.Args[0];
            string @operator = _getOperator();
            object rightVal = context.Args[1];
            bool? comparisonResult = EvalisUtil.Compare(leftVal, @operator, rightVal, (context as FormulaContext).Evaluator.SupportedLogicalOperators);

            if (comparisonResult.HasValue)
            {
                return EvaluationResult.WithValue(comparisonResult.Value);
            }
            else
            {
                return EvaluationResult.WithError(Error.Value($"Invalid operator/operands used in expression. '{leftVal} {@operator} {rightVal}'."));
            }
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