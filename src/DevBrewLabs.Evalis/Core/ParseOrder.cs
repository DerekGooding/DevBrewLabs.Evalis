using System.Collections;
using System.Collections.Generic;

namespace DevBrewLabs.Evalis
{
    /// <summary>
    /// A concrete implementation of IParseOrder that defines an ordered set of ParseType values for argument resolution.
    /// </summary>
    public class ParseOrder : IParseOrder
    {
        private readonly HashSet<ParseType> _order;

        /// <summary>
        /// Initializes a new ParseOrder with a single ParseType.
        /// </summary>
        /// <param name="firstParseType">The first parse type in the order.</param>
        public ParseOrder(ParseType firstParseType)
        {
            _order = new HashSet<ParseType>();
            Add(firstParseType);
        }

        /// <summary>
        /// Initializes a new ParseOrder from an existing sequence of ParseTypes.
        /// </summary>
        /// <param name="parseTypes">The parse types to include.</param>
        public ParseOrder(IEnumerable<ParseType> parseTypes) => _order = new HashSet<ParseType>(parseTypes);

        /// <summary>
        /// Adds a ParseType to the order.
        /// </summary>
        /// <param name="mode">The ParseType to add.</param>
        public void Add(ParseType mode) => _order.Add(mode);

        public IEnumerator<ParseType> GetEnumerator() => _order.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _order.GetEnumerator();
    }
}