using AlphaX.Parserz;

namespace AlphaX.FormulaEngine
{
    public interface IEvaluator
    {
        /// <summary>
        /// Evaluates the result from AST.
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        object Evaluate(IParserResult result);
    }
}
