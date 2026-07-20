using System.Collections.Generic;
using AlphaX.Parserz;

namespace AlphaX.FormulaEngine
{
    public interface IEngineSettings
    {
        /// <summary>
        /// Gets or sets whether the engine supports single/double quote for string inputs. (Default is true).
        /// </summary>
        bool DoubleQuotedStrings { get; set; }
        /// <summary>
        /// Gets or sets the parse order for engine. The engine will try to parse formula argument in the specified order. This could be used to improve engine performance. For example, Number could be specified first in parse order if the formulas to be used only uses numeric arguments.
        /// </summary>
        IParseOrder EngineParseOrder { get; set; }
        /// <summary>
        /// Gets or sets the logical operator mode. For example, 'eq' instead of '=', 'ne' instead of '!=' etc.
        /// </summary>
        LogicalOperatorMode LogicalOperatorMode { get; set; }
        /// <summary>
        /// Gets or sets a list of custom parsers for resolving user-defined variables/tokens. 
        /// If provided, these parsers will be evaluated as CustomNames.
        /// </summary>
        List<IParser> CustomTokenParsers { get; set; }
    }
}
