using System;

namespace AlphaX.FormulaEngine
{
    public static class SequencedExpressionBuilder
    {
        public static SequencedExpression Create(string key, string expression)
        {
            SequencedExpression seqExpression = new SequencedExpression();
            seqExpression.AddSegment(new SequencedExpressionSegment(key, expression));
            return seqExpression;
        }

        public static SequencedExpression Next(this SequencedExpression seqExpression, string key, string expression)
        {
            SequencedExpressionSegment previousSegment = seqExpression.GetPreviousSegment();

            if (!expression.Contains(previousSegment.Key))
            {
                throw new InvalidOperationException($"Previous segment key ({previousSegment.Key}) should be present in the expression segment.");
            }

            seqExpression.AddSegment(new SequencedExpressionSegment(key, expression));
            return seqExpression;
        }
    }
}
