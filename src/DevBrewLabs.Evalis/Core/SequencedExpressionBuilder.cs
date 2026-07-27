namespace DevBrewLabs.Evalis
{
    public static class SequencedExpressionBuilder
    {
        public static ISequencedExpression Create(string key, string expression)
        {
            SequencedExpression seqExpression = new SequencedExpression();
            seqExpression.AddSegment(new SequencedExpressionSegment(key, expression));
            return seqExpression;
        }

        public static ISequencedExpression Next(this ISequencedExpression seqExpression, string key, string expression)
        {
            (seqExpression as SequencedExpression)?.AddSegment(new SequencedExpressionSegment(key, expression));
            return seqExpression;
        }
    }
}