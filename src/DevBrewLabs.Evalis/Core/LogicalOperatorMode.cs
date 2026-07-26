namespace DevBrewLabs.Evalis
{
    /// <summary>
    /// Specifies the syntax mode used for logical operators in formula expressions.
    /// </summary>
    public enum LogicalOperatorMode
    {
        /// <summary>
        /// Standard symbolic operators (e.g., =, !=, &amp;&amp;, ||, &lt;, &gt;).
        /// </summary>
        Default,
        /// <summary>
        /// Query-style textual operators (e.g., eq, ne, and, or, lt, gt).
        /// </summary>
        Query
    }
}