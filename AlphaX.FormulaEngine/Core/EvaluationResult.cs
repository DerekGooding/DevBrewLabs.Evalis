namespace AlphaX.FormulaEngine
{
    /// <summary>
    /// Internal implementation of IEvaluationResult, carrying either a successful value or an error message.
    /// </summary>
    internal class EvaluationResult : IEvaluationResult
    {
        public object Value { get; private set; }
        public string Error { get; private set; }

        /// <summary>
        /// Initializes a successful result.
        /// </summary>
        /// <param name="value">The evaluated value.</param>
        public static EvaluationResult WithValue(object value)
        {
            return new EvaluationResult() { Value = value };
        }

        /// <summary>
        /// Initializes a failed result.
        /// </summary>
        /// <param name="error">The error message.</param>
        public static EvaluationResult WithError(string error)
        {
            return new EvaluationResult() { Error = error };
        }
    }
}
