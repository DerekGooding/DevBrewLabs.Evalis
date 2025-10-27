using System.Threading.Tasks;

namespace AlphaX.FormulaEngine
{
    public interface IEngineContext
    {
        Task<object> Resolve(string key);
    }
}
