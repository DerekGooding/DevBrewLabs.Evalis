using DevBrewLabs.Parserly;
using NUnit.Framework;

namespace DevBrewLabs.Evalis.Tests;

internal class SpreadsheetTokenParser : RegexParser<StringResult>
{
    public SpreadsheetTokenParser(string pattern) : base(new System.Text.RegularExpressions.Regex(pattern, System.Text.RegularExpressions.RegexOptions.Compiled), true)
    {
    }

    protected override StringResult ConvertResult(System.Text.RegularExpressions.Match value) => new StringResult(value.Value);

    protected override IParserError CreateError(int index, string value) => new ParserError(index, "Unexpected custom token");
}

[TestFixture]
public class SpreadsheetIntegrationTests
{
    [Test]
    public void SpreadsheetTokens_Are_Extracted_Successfully()
    {
        var settings = new EngineSettings();
        settings.CustomTokenParsers = new List<IParser>
        {
            new SpreadsheetTokenParser(@"^[A-Za-z0-9_]+![A-Za-z]+[0-9]+:[A-Za-z]+[0-9]+"), // Sheet1!A1:B10
            new SpreadsheetTokenParser(@"^[A-Za-z0-9_]+![A-Za-z]+[0-9]+"), // Sheet1!A1
            new SpreadsheetTokenParser(@"^[A-Za-z]+[0-9]+:[A-Za-z]+[0-9]+"), // A1:B10
            new SpreadsheetTokenParser(@"^[A-Za-z]+[0-9]+") // A1
        };

        var engine = new FormulaEngine();
        engine.ApplySettings(settings);

        var variables = engine.ExtractVariables("SUM(Sheet1!A1:B10, A2, Sheet2!C4)");

        Assert.That(variables.Length, Is.EqualTo(3));
        Assert.That(variables[0], Is.EqualTo("Sheet1!A1:B10"));
        Assert.That(variables[1], Is.EqualTo("A2"));
        Assert.That(variables[2], Is.EqualTo("Sheet2!C4"));
    }
}