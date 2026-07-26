namespace DevBrewLabs.Evalis
{
    /// <summary>
    /// Specifies the type of value the engine will attempt to parse for a formula argument.
    /// </summary>
    public enum ParseType
    {
        /// <summary>
        /// A user-defined variable or token resolved via the engine context.
        /// </summary>
        CustomName = 1,
        /// <summary>
        /// A string literal value.
        /// </summary>
        String = 2,
        /// <summary>
        /// A numeric (double) value.
        /// </summary>
        Number = 3,
        /// <summary>
        /// A boolean (true/false) value.
        /// </summary>
        Boolean = 4,
        /// <summary>
        /// A nested formula expression.
        /// </summary>
        Formula = 5
    }
}
