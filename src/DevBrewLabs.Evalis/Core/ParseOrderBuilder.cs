namespace DevBrewLabs.Evalis
{
    /// <summary>
    /// Provides a fluent API for constructing an IParseOrder, controlling the order in which the engine attempts to parse formula arguments.
    /// </summary>
    public static class ParseOrderBuilder
    {
        /// <summary>
        /// The default parse order: Number → String → Boolean → CustomName → Formula.
        /// </summary>
        public static IParseOrder DefaultParseOrder { get; }

        static ParseOrderBuilder()
        {
            DefaultParseOrder = FirstParse(ParseType.Number)
               .AndThenParse(ParseType.String)
               .AndThenParse(ParseType.Boolean)
               .AndThenParse(ParseType.CustomName)
               .AndThenParse(ParseType.Formula);
        }

        /// <summary>
        /// Creates a new IParseOrder starting with the specified ParseType.
        /// </summary>
        /// <param name="firstParse">The first parse type to attempt.</param>
        /// <returns>A new IParseOrder containing only the specified parse type.</returns>
        public static IParseOrder FirstParse(ParseType firstParse)
        {
            return new ParseOrder(firstParse);
        }

        /// <summary>
        /// Extends an existing IParseOrder by appending an additional ParseType.
        /// </summary>
        /// <param name="parseOrder">The existing parse order to extend.</param>
        /// <param name="parseType">The additional parse type to append.</param>
        /// <returns>A new IParseOrder with the additional parse type appended.</returns>
        public static IParseOrder AndThenParse(this IParseOrder parseOrder, ParseType parseType)
        {
            var newOrder = new ParseOrder(parseOrder);
            newOrder.Add(parseType);
            return newOrder;
        }
    }
}