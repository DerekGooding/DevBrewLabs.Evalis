using AlphaX.FormulaEngine.Resources;
using AlphaX.FormulaEngine.Utils;
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

        public override object Evaluate(params object[] args)
        {
            if (!args.TryGetArgument(0, out object left))
            {
                throw new ArgumentException(string.Format(FormulaResources.InvalidObjectArgument, 0));
            }

            if (!args.TryGetArgument(1, out object right))
            {
                throw new ArgumentException(string.Format(FormulaResources.InvalidObjectArgument, 1));
            }

            return AlphaXComparer.Compare(left, _getOperator(), right, Engine.Evaluator.SupportedLogicalOperators);
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
