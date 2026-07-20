using System;

namespace AlphaX.FormulaEngine
{
    /// <summary>
    /// Thrown when an error occurs during formula evaluation (e.g. invalid operands, unsupported operator).
    /// </summary>
    public class EvaluationException : Exception
    {
        /// <summary>
        /// Initializes a new EvaluationException.
        /// </summary>
        /// <param name="message">The exception message.</param>
        public EvaluationException(string message) : base(message)
        {

        }
    }

    /// <summary>
    /// General-purpose exception for errors originating from the AlphaX Formula Engine.
    /// </summary>
    public class AlphaXFormulaEngineException : Exception
    {
        /// <summary>
        /// Initializes a new AlphaXFormulaEngineException.
        /// </summary>
        /// <param name="message">The exception message.</param>
        public AlphaXFormulaEngineException(string message) : base(message)
        {

        }
    }
}
