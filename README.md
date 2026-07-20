# AlphaX.FormulaEngine

A robust, extensible, and blazing fast engine to parse and evaluate formulas dynamically. 
Built on top of [AlphaX.Parserz](https://www.nuget.org/packages/AlphaX.Parserz), it seamlessly supports both natively provided formulas and your own custom logic.

🔗 [AlphaX.FormulaEngine GitHub Repo](https://github.com/kartikdeepsagar/AlphaX.FormulaEngine)

---

## 🚀 What's New in v3.3.1
We are excited to bring 24 new built-in formulas and a massive architectural improvement to AlphaX.FormulaEngine!
- **[BREAKING CHANGE] Exception-Free Error Pipeline:** To drastically improve performance and support error-handling formulas (like `IFERROR`), formulas no longer throw C# exceptions to propagate errors.
  - Custom formulas must now return `IEvaluationResult` (using `EvaluationResult.WithValue(...)` or `EvaluationResult.WithError(...)`) instead of `object`.
- **24 New Built-in Formulas:** Added comprehensive support for heavily requested formulas including `MOD`, `TRUNC`, `SIGN`, `LOG`, `LOG10`, `EXP`, `PI`, `IFS`, `SWITCH`, `IFERROR`, `IFBLANK`, `ISBOOL`, `ISDATE`, `ISARRAY`, `ISNULL`, `LEFT`, `RIGHT`, `MID`, `PAD`, `REPEAT`, `PROPER`, `ISEMPTY`, `ISNULLOREMPTY`, and `FORMAT`.
- **Case-Insensitive Formulas:** Formula names are now completely case-insensitive (e.g. `sum`, `SUM`, and `sUM` all map to the same formula), providing a seamless, Excel-like user experience.
- **Parser Fix for Alphanumerics:** Fixed a bug in the AST parser where variables or formulas ending in numbers (e.g. `LOG10`) were parsed incorrectly.

---

## ⚡ Quick Start

You can initialize the engine and evaluate expressions either synchronously or asynchronously:

```csharp
using AlphaX.FormulaEngine;

AlphaXFormulaEngine engine = new AlphaXFormulaEngine();

// Synchronous Evaluation
IEvaluationResult resultSync = engine.Evaluate("SUM(1, 2, 12.3, 5.9)");
Console.WriteLine(resultSync.Value); // 21.2

// Asynchronous Evaluation
IEvaluationResult resultAsync = await engine.EvaluateAsync("SUM(1, 2, 12.3, 5.9)");
Console.WriteLine(resultAsync.Value); // 21.2
```

> **Pro Tip:** Formulas can naturally nest! Feel free to parse deep trees natively: `(SUM(1,2) + AVERAGE(5,10)) * 10`.

---

## 📚 Inbuilt Formulas

AlphaX.FormulaEngine ships with a wide array of powerful formulas right out of the box:

- **🧮 Arithmetic**: `SUM`, `AVERAGE`, `FLOOR`, `ROUND`, `MIN`, `MAX`, `POWER`, `SQRT`
- **🔤 String**: `LOWER`, `UPPER`, `TEXTSPLIT`, `CONCAT`, `LENGTH`, `TRIM`, `SUBSTRING`, `INDEXOF`
- **📅 DateTime**: `TODAY`, `NOW`, `YEAR`, `MONTH`, `DAY`
- **🧠 Logical**: `EQUALS`, `GREATERTHAN`, `OR`, `AND`, `IF`, `COALESCE`, `ISNUMBER`, `ISSTRING`
- **📦 Array**: `ARRAYCONTAINS`, `ARRAYINCLUDES`, `INDEX`, `JOIN`, `COUNT`

👉 **[Click here to see the full list of inbuilt formulas and examples](https://github.com/kartikdeepsagar/AlphaX.FormulaEngine/blob/master/Formulas.md)**

---

## 🛠 Creating Your Own Formulas

AlphaX.FormulaEngine provides maximum flexibility to write and integrate your own custom logic effortlessly.

### 1. Create a `Formula` Class
Inherit from `AlphaX.FormulaEngine.Formula`. Below is a custom `StartsWith` formula implementation:

```csharp
public class StartsWithFormula : AlphaX.FormulaEngine.Formula
{
    public StartsWithFormula() : base("StartsWith") { }

    public override IEvaluationResult Evaluate(IFormulaContext context)
    {
        // Throws error if argument count doesn't match
        ValidateArgumentCount(context.Args); 

        // Throws error if 0th/1st arguments aren't strings
        string source = context.GetStringArg(0); 
        string value = context.GetStringArg(1);

        // Safely retrieves the 3rd argument, or defaults to false
        context.TryGetArg(2, out bool matchCase); 
        
        bool result = source.StartsWith(value, matchCase ? StringComparison.Ordinal : StringComparison.InvariantCultureIgnoreCase);
        return EvaluationResult.WithValue(result);
    }

    protected override FormulaInfo GetFormulaInfo()
    {
        FormulaInfo info = new FormulaInfo(Name)
        {
            Description = "Checks if the provided string starts with the specified value."
        };

        // Define arguments for function documentation/validation
        info.AddArgument(new StringArgument("source", true) { Description = "The source string." });
        info.AddArgument(new StringArgument("value", true) { Description = "The value to check for." });
        info.AddArgument(new BooleanArgument("matchCase", false) { Description = "Match case while checking." });
        
        return info;
    }
}
```

### 2. Register & Evaluate
Simply add your formula to the `FormulaStore` and it is immediately ready for use!

```csharp
AlphaXFormulaEngine engine = new AlphaXFormulaEngine();
engine.FormulaStore.Add(new StartsWithFormula());

var result1 = engine.Evaluate("StartsWith(\"This is test\", \"This\")");
Console.WriteLine(result1.Value); // true

var result2 = engine.Evaluate("StartsWith(\"This is test\", \"hello\")");
Console.WriteLine(result2.Value); // false
```

### 3. Asynchronous Formulas

Need to fetch data from an API or database during evaluation? Simply inherit from `AlphaX.FormulaEngine.AsyncFormula`:

```csharp
public class FetchDataFormula : AlphaX.FormulaEngine.AsyncFormula
{
    public FetchDataFormula() : base("FETCH") { }

    public override async Task<IEvaluationResult> EvaluateAsync(IFormulaContext context)
    {
        string url = context.GetStringArg(0);
        
        // Example: await your async calls natively!
        string result = await MyHttpClient.GetAsync(url); 
        
        return EvaluationResult.WithValue(result);
    }
    
    // ... GetFormulaInfo() omitted for brevity
}
```

> **Note:** To evaluate an AST that contains an async formula, you must execute the engine via `await engine.EvaluateAsync(...)` instead of the synchronous `engine.Evaluate(...)`.

---

## ⚙️ Advanced Configuration

AlphaXFormulaEngine allows you to configure the engine to fit your exact domain needs.

### 1. Toggle String Quotes
By default, strings are parsed with double quotes (`"text"`). You can toggle this to accept single quotes (`'text'`) by updating the engine settings:
```csharp
engine.ApplySettings(new EngineSettings()
{
     DoubleQuotedStrings = false
});
```

### 2. Logical Operator Modes
You can configure the engine to parse query-like operators (`eq` instead of `=`) via `LogicalOperatorMode`.
```csharp
engine.ApplySettings(new EngineSettings()
{
     LogicalOperatorMode = LogicalOperatorMode.Query
});
```
**Query Mode Operators:**
- `=` → `eq` | `!=` → `ne`
- `<` → `lt` | `>` → `gt`
- `<=` → `le` | `>=` → `ge`
- `&&` → `and` | `||` → `or`

### 3. Parsing Optimization Order
You can manually sequence the type resolution tree (e.g. parse Numbers before Strings) to drastically improve performance if you know your data bounds:
```csharp
ParseOrder order = new ParseOrder(ParseType.Number);
order.Add(ParseType.String);
order.Add(ParseType.Boolean);

engine.ApplySettings(new EngineSettings() { EngineParseOrder = order });
```

---

## 🎯 Variables & Dependency Extraction

AlphaX.FormulaEngine allows you to inject variables directly into expressions by providing a custom `IEngineContext` to resolve their values at runtime.

### Standard Variables
By default, variables are prefixed with `$`:

```csharp
public class TestEngineContext : IEngineContext
{
    public async Task<object> Resolve(string key)
    {
        return key switch
        {
            "UserId" => 1024,
            "Role" => "Admin",
            _ => throw new Exception("Invalid variable name")
        };
    }
}

AlphaXFormulaEngine engine = new AlphaXFormulaEngine(new TestEngineContext());
IEvaluationResult result = engine.Evaluate("EQUALS($UserId, 1024)"); // true
```

### Custom Token Parsers
If you are building a complex rule engine, you may want to parse variables without the `$` prefix, for example `[Col Name]` or cell references like `A1:B10`. You can inject `CustomTokenParsers` into the engine settings:

```csharp
using AlphaX.Parserz;

// Create a custom RegexParser
public class MyCustomTokenParser : RegexParser<StringResult>
{
    public MyCustomTokenParser(string pattern) 
        : base(new System.Text.RegularExpressions.Regex(pattern, System.Text.RegularExpressions.RegexOptions.Compiled), true) { }

    protected override StringResult ConvertResult(System.Text.RegularExpressions.Match value) => new StringResult(value.Value);
    protected override IParserError CreateError(int index, string value) => new ParserError(index, "Unexpected custom token");
}

// Apply settings
var settings = new EngineSettings();
settings.CustomTokenParsers = new List<IParser>
{
    new MyCustomTokenParser(@"^\[[A-Za-z0-9_ ]+\]"), // e.g. [Column Name]
    new MyCustomTokenParser(@"^[A-Za-z]+[0-9]+:[A-Za-z]+[0-9]+") // e.g. A1:B10
};
engine.ApplySettings(settings);

// The engine now parses these custom tokens as Variables!
// They will be passed directly into your `IEngineContext.Resolve` method during evaluation!
```

### AST Dependency Extraction
For building dependency graphs (e.g. knowing which fields to recalculate when a variable changes), you can statically extract all parsed variables from a formula *without* evaluating it:

```csharp
var variables = engine.ExtractVariables("SUM([Tax], A2, [Subtotal])");

// variables: ["[Tax]", "A2", "[Subtotal]"]
```

---

## 🔗 Sequenced / Chained Expressions

Evaluating massive, complicated expression walls can be extremely difficult to read or debug (e.g. `SUM(1, 2, AVERAGE(1, 2, SUM(1, 2, 12)))`).

AlphaX provides a `SequencedExpressionBuilder` to break these down natively into readable variables:

```csharp
var engine = new AlphaXFormulaEngine();

var expression = SequencedExpressionBuilder
    .Create("Step1", "SUM(1, 2, 12)")
    .Next("Step2", "AVERAGE(1, 2, $Step1)")
    .Next("Final", "SUM(1, 2, $Step2)");

var result = engine.Evaluate(expression); 
// Final Result evaluates properly by cascading through the sequenced variables!
```

---

*Built by developers, for developers :-)*