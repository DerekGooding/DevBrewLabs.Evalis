namespace DevBrewLabs.Evalis
{
    public interface IEvaluationResult
    {
        /// <summary>
        /// Gets the result value.
        /// </summary>
        object Value { get; }

        /// <summary>
        /// Gets the evaluation error if any.
        /// </summary>
        Error? Error { get; }
    }
}