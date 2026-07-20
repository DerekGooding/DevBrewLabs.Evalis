using System.Collections.Generic;

namespace AlphaX.FormulaEngine
{
    /// <summary>
    /// Defines the ordered sequence of parse types the engine will attempt when resolving formula arguments.
    /// </summary>
    public interface IParseOrder : IEnumerable<ParseType>
    {

    }
}