namespace AlphaX.FormulaEngine
{
    internal class LogicalOperators
    {
        public string EqualsTo = "=";
        public string NotEquals = "!=";
        public string LessThan = "<";
        public string GreaterThan = ">";
        public string LessThanEqualsTo = "<=";
        public string GreaterThanEqualsTo = ">=";
        public string AND = "&&";
        public string OR = "||";

        public LogicalOperators(LogicalOperatorMode mode)
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