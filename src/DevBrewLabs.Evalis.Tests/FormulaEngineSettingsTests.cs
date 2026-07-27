using NUnit.Framework;

namespace DevBrewLabs.Evalis.Tests;

public class FormulaEngineSettingsTests
{
    private IFormulaEngine _formulaEngine;

    [OneTimeSetUp]
    public void Setup() => _formulaEngine = new FormulaEngine();
}