using System.Collections.Generic;

namespace DevBrewLabs.Evalis
{
    /// <summary>
    /// Defines the ordered sequence of parse types the engine will attempt when resolving formula arguments.
    /// </summary>
    public interface IParseOrder : IEnumerable<ParseType>
    {

    }
}