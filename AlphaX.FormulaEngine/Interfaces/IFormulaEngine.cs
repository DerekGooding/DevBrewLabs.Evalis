using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlphaX.FormulaEngine
{
    public interface IFormulaEngine
    {
        /// <summary>
        /// Gets the engine context.
        /// </summary>
        IEngineContext Context { get; set; }
        /// <summary>
        /// Gets the formula store.
        /// </summary>
        IFormulaStore FormulaStore { get; }
        /// <summary>
        /// Gets the evaluated result of the provided formula expression.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        IEvaluationResult Evaluate(string input);
        /// <summary>
        /// Gets the evaluated result of the provided sequenced formula expression.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        IEvaluationResult Evaluate(ISequencedExpression input);
        /// <summary>
        /// Gets the async evaluated result of the provided formula expression.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<IEvaluationResult> EvaluateAsync(string input);
        /// <summary>
        /// Gets the async evaluated result of the provided sequenced formula expression.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<IEvaluationResult> EvaluateAsync(ISequencedExpression input);
        /// <summary>
        /// Applies settings to the engine.
        /// </summary>
        /// <param name="settings"></param>
        void ApplySettings(IEngineSettings settings);
    }
}
