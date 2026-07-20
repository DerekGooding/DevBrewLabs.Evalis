namespace AlphaX.FormulaEngine
{
    /// <summary>
    /// Internal implementation of IEvaluationResult, carrying either a successful value or an error message.
    /// </summary>
    internal class EvaluationResult : IEvaluationResult
    {
        public object Value { get; }
        public string Error { get; }

        /// <summary>
        /// Initializes a successful result.
        /// </summary>
        /// <param name="value">The evaluated value.</param>
        public EvaluationResult(object value)
        {
            Value = value;
        }

        /// <summary>
        /// Initializes a failed result.
        /// </summary>
        /// <param name="error">The error message.</param>
        public EvaluationResult(string error)
        {
            Error = error;
        }
    }
}
