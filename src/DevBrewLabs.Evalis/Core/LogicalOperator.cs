namespace DevBrewLabs.Evalis
{
    internal class LogicalOperator
    {
        public string EqualsTo { get; } = "=";
        public string NotEquals { get; } = "!=";
        public string LessThan { get; } = "<";
        public string GreaterThan { get; } = ">";
        public string LessThanEqualsTo { get; } = "<=";
        public string GreaterThanEqualsTo { get; } = ">=";
        public string AND { get; } = "&&";
        public string OR { get; } = "||";

        public LogicalOperator(LogicalOperatorMode mode)
        {
            if (mode == LogicalOperatorMode.Query)
            {
                EqualsTo = "eq";
                NotEquals = "ne";
                LessThan = "lt";
                GreaterThan = "gt";
                LessThanEqualsTo = "le";
                GreaterThanEqualsTo = "ge";
                AND = "and";
                OR = "or";
            }
        }
    }
}