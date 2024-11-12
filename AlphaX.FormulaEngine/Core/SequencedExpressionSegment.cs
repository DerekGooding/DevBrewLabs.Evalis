using System;

namespace AlphaX.FormulaEngine
{
    public sealed class SequencedExpressionSegment
    {
        public string Key { get; }
        public string Expression { get; }
        internal object Result {  get; set; }

        public SequencedExpressionSegment(string key, string expression)
        {
            Key = key;
            Expression = expression;
        }
    }
}
