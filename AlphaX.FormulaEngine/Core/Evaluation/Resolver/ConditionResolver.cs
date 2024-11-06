using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaX.FormulaEngine.Core.Evaluation.Resolver
{
    internal class ConditionResolver : ArgumentResolver<Condition, bool>
    {
        public ConditionResolver(AlphaXFormulaEngine engine) : base(engine)
        {
        }

        public override bool Resolve(Condition input)
        {
            var left = Engine.Evaluator.Evaluate(input.LeftOperand);
            var @operator = Engine.Evaluator.Evaluate(input.Operator);
            var right = Engine.Evaluator.Evaluate(input.RightOperand);
            return AlphaXComparer.Compare(left, @operator?.ToString(), right, Engine.SupportedLogicalOperators);
        }
    }
}
