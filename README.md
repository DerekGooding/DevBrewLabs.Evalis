# DevBrewLabs.Evalis

A robust, extensible, and blazing fast engine to parse and evaluate formulas dynamically. 
Built on top of [DevBrewLabs.Parserly](https://www.nuget.org/packages/DevBrewLabs.Parserly), it seamlessly supports both natively provided formulas and your own custom logic.

🔗 [DevBrewLabs.Evalis GitHub Repo](https://github.com/kartikdeepsagar/DevBrewLabs.Evalis)

> [!NOTE]
> **📢 Rebranding Notice**
> `AlphaX.FormulaEngine` has been officially rebranded to **`DevBrewLabs.Evalis`**.
> - **Package Name:** `AlphaX.FormulaEngine` ➔ **`DevBrewLabs.Evalis`**
> - **Core Engine Class:** `AlphaXFormulaEngine` ➔ **`FormulaEngine`**
> - **Parser Dependency:** `AlphaX.Parserz` ➔ **`DevBrewLabs.Parserly`**
> - **Namespace:** `AlphaX.FormulaEngine` ➔ **`DevBrewLabs.Evalis`**

---

## 🚀 What's New in v3.4.0
- **Structured Error Handling:** Evaluation errors now safely return an `Error` struct inside `IEvaluationResult` rather than throwing expensive C# exceptions.
- **Variadic Arguments (`isVariadic`):** Formulas can now natively support infinite comma-separated arguments (e.g., `SUM`, `CONCAT`, `IFS`) by using the `isVariadic: true` parameter when defining a `FormulaArgument`.
- **Centralized Argument Validation:** Formula argument counts are now securely and automatically validated by the engine prior to execution, removing boilerplate code from custom formulas.

> [!WARNING]
> **Breaking Changes**
> * **Exception Handling:** The engine no longer throws `EvaluationException` when a formula encounters an error (e.g., invalid arguments). Instead, it returns an `IEvaluationResult` with the new `Error` struct populated. Update your code to check `if (result.Error.HasValue)` instead of using a `try-catch` block.
> * **Custom Formulas:** The `ValidateArgumentCount()` method has been removed from `Formula`. The engine now handles validation for you automatically before `Evaluate()` is ever called. If you have custom formulas, you must remove any calls to this base method.




---

## ⚡ Quick Start

You can initialize the engine and evaluate expressions either synchronously or asynchronously:

```csharp
using DevBrewLabs.Evalis;

FormulaEngine engine = new FormulaEngine();

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

DevBrewLabs.Evalis ships with a wide array of powerful formulas right out of the box:

- **🧮 Arithmetic**: `SUM`, `AVERAGE`, `FLOOR`, `ROUND`, `MIN`, `MAX`, `POWER`, `SQRT`
- **🔤 String**: `LOWER`, `UPPER`, `TEXTSPLIT`, `CONCAT`, `LENGTH`, `TRIM`, `SUBSTRING`, `INDEXOF`, `REGEXMATCH`
- **📅 DateTime**: `TODAY`, `NOW`, `YEAR`, `MONTH`, `DAY`, `DATETIME`
- **🧠 Logical**: `EQUALS`, `GREATERTHAN`, `OR`, `AND`, `IF`, `COALESCE`, `ISNUMBER`, `ISSTRING`
- **📦 Array**: `ARRAYCONTAINS`, `ARRAYINCLUDES`, `INDEX`, `JOIN`, `COUNT`

👉 **[Click here to see the full list of inbuilt formulas and examples](https://github.com/kartikdeepsagar/DevBrewLabs.Evalis/blob/master/Formulas.md)**

---

## 🛠 Creating Your Own Formulas

DevBrewLabs.Evalis provides maximum flexibility to write and integrate your own custom logic effortlessly.

### 1. Create a `Formula` Class
Inherit from `DevBrewLabs.Evalis.Formula`. Below is a custom `StartsWith` formula implementation:

```csharp
public class StartsWithFormula : DevBrewLabs.Evalis.Formula
{
    public StartsWithFormula() : base("StartsWith") { }

    public override IEvaluationResult Evaluate(IFormulaContext context)
    {
        // Argument counts are now automatically validated by the engine!
        // Retrieve arguments safely using the context methods:
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
FormulaEngine engine = new FormulaEngine();
engine.FormulaStore.Add(new StartsWithFormula());

var result1 = engine.Evaluate("StartsWith(\"This is test\", \"This\")");
Console.WriteLine(result1.Value); // true

var result2 = engine.Evaluate("StartsWith(\"This is test\", \"hello\")");
Console.WriteLine(result2.Value); // false
```

### 3. Asynchronous Formulas

Need to fetch data from an API or database during evaluation? Simply inherit from `DevBrewLabs.Evalis.AsyncFormula`:

```csharp
public class FetchDataFormula : DevBrewLabs.Evalis.AsyncFormula
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

FormulaEngine allows you to configure the engine to fit your exact domain needs.

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

DevBrewLabs.Evalis allows you to inject variables directly into expressions by providing a custom `IEngineContext` to resolve their values at runtime.

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

FormulaEngine engine = new FormulaEngine(new TestEngineContext());
IEvaluationResult result = engine.Evaluate("EQUALS($UserId, 1024)"); // true
```

### Custom Token Parsers
If you are building a complex rule engine, you may want to parse variables without the `$` prefix, for example `[Col Name]` or cell references like `A1:B10`. You can inject `CustomTokenParsers` into the engine settings:

```csharp
using DevBrewLabs.Parserly;

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

Evalis provides a `SequencedExpressionBuilder` to break these down natively into readable variables:

```csharp
var engine = new FormulaEngine();

var expression = SequencedExpressionBuilder
    .Create("Step1", "SUM(1, 2, 12)")
    .Next("Step2", "AVERAGE(1, 2, $Step1)")
    .Next("Final", "SUM(1, 2, $Step2)");

var result = engine.Evaluate(expression); 
// Final Result evaluates properly by cascading through the sequenced variables!
```

---

*Built by developers, for developers :-)*