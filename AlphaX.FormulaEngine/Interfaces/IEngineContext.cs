using System.Threading.Tasks;

namespace AlphaX.FormulaEngine
{
    /// <summary>
    /// Represents the resolution context for the formula engine, used to resolve variable/token values by name.
    /// </summary>
    public interface IEngineContext
    {
        /// <summary>
        /// Resolves a variable or token by its name.
        /// </summary>
        /// <param name="key">The name of the variable or token to resolve.</param>
        /// <returns>The resolved value, or null if not found.</returns>
        Task<object> Resolve(string key);
    }
}
