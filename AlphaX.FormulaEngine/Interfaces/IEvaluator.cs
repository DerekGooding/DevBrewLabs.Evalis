using AlphaX.Parserz;
using System.Threading.Tasks;

namespace AlphaX.FormulaEngine
{
    public interface IEvaluator
    {
        /// <summary>
        /// Evaluates the result from AST.
        /// </summary>
        /// <param name="result"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        Task<object> Evaluate(IParserResult result, IEngineContext context);
    }
}
