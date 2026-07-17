using NUnit.Framework;

namespace AlphaX.FormulaEngine.Tests
{
    public class FormulaEngineSettingsTests
    {
        private IFormulaEngine _formulaEngine;

        [OneTimeSetUp]
        public void Setup()
        {
            _formulaEngine = new AlphaXFormulaEngine();
        }
    }
}
