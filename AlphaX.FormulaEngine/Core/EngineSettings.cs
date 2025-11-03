using System;
using System.Linq;

namespace AlphaX.FormulaEngine
{
    public class EngineSettings : IEngineSettings
    {
        public bool DoubleQuotedStrings { get; set; }
        public IParseOrder EngineParseOrder { get; set; }
        public LogicalOperatorMode LogicalOperatorMode { get; set; }

        public EngineSettings()
        {
            LogicalOperatorMode = LogicalOperatorMode.Default;
            DoubleQuotedStrings = true;
            EngineParseOrder = ParseOrderBuilder.DefaultParseOrder;
        }
    }
}
