
# AlphaX.FormulaEngine

A strong and fast library to parse and evaluate formulas. It also supports custom formulas.  
This library is built using [AlphaX.Parserz](https://www.nuget.org/packages/AlphaX.Parserz).

GitHub Repo: [https://github.com/kartikdeepsagar/AlphaX.FormulaEngine](https://github.com/kartikdeepsagar/AlphaX.FormulaEngine)  
Feedback: [https://forms.gle/dfv8E8zpC2qPJS7i7](https://forms.gle/dfv8E8zpC2qPJS7i7)

---

## Using AlphaXFormulaEngine

You can initialize the engine and evaluate formulas like this:

```csharp
AlphaX.FormulaEngine.AlphaXFormulaEngine engine = new AlphaX.FormulaEngine.AlphaXFormulaEngine();
AlphaX.FormulaEngine.IEvaluationResult result = engine.Evaluate("SUM(1,2,12.3,5.9099)");
Console.WriteLine(result.Value); // 21.2099
```

---

## Inbuilt Formulas

### Arithmetic Formulas
- **SUM** – Returns the sum of provided values.  
  Example: `SUM(1,2,4)` → 7  
- **AVERAGE** – Returns the average of provided values.  
  Example: `AVERAGE(3,2,4)` → 3  
- **CEILING** – Returns the smallest integer ≥ specified number.  
  Example: `CEILING(1.34)` → 2  
- **FLOOR** – Returns the largest integer ≤ specified number.  
  Example: `FLOOR(1.34)` → 1  
- **ROUND** – Rounds to a specified number of decimal places.  
  Example: `ROUND(1.34234, 2)` → 1.35  

### String Formulas
- **LOWER** – Converts string to lowercase.  
  Example: `LOWER("TESTSTRING")` → teststring  
- **UPPER** – Converts string to uppercase.  
  Example: `UPPER("teststring")` → TESTSTRING  
- **TEXTSPLIT** – Splits string by separator.  
  Example: `TEXTSPLIT(".", "John.Doe")` → John Doe  
- **CONCAT** – Joins multiple strings.  
  Example: `CONCAT("Test","String","1")` → TestString1  
- **LENGTH** – Gets string length.  
  Example: `LENGTH("AlphaX")` → 6  
- **CONTAINS** – Checks if string contains another string.  
  Example: `CONTAINS("AlphaX", "pha")` → true  
- **STARTSWITH** – Checks if string starts with given string.  
  Example: `STARTSWITH("AlphaX", "Al", true)` → true  
- **ENDSWITH** – Checks if string ends with given string.  
  Example: `ENDSWITH("AlphaX", "ax")` → true  
- **REPLACE** – Replaces substring with another.  
  Example: `REPLACE("test test", "test", "best", false)` → best test  

### DateTime Formulas
- **TODAY** – Returns system date.  
  Example: `TODAY()` → 28-04-2023  
- **NOW** – Returns system date and time.  
  Example: `NOW()` → 28-04-2023 10:52:53 PM  
- **DATETIME** – Parses a datetime string.  
  Example: `DATETIME("2024/01/01")`  

### Logical Formulas
- **EQUALS** – Checks equality.  
  Example: `EQUALS(true, 1 > 3)` → false  
- **GREATERTHAN** – Checks if one value is greater.  
  Example: `GREATERTHAN(5,2)` → true  
- **LESSTHAN** – Checks if one value is less.  
  Example: `LESSTHAN(5,2)` → false  
- **NOT** – Negates a boolean value.  
  Example: `NOT(1 == 1)` → false  
- **AND** – Logical AND.  
  Example: `AND(true, 1 != 1)` → false  
- **OR** – Logical OR.  
  Example: `OR(true, 1 != 1)` → true  
- **IF** – Conditional logic.  
  Example: `IF(UPPER("alphax") = UPPER("ALphaX"), true, false)` → true  

### Array Formulas
- **ARRAYCONTAINS** – Checks if array contains a value.  
  Example: `ARRAYCONTAINS([1,2,3], 2)` → true  
- **ARRAYINCLUDES** – Checks if array includes all values.  
  Example: `ARRAYINCLUDES([1,2,3,4], [3,4])` → true  

> **Note:** More formulas will be added in future updates.

---

## Creating a Custom Formula

### Example: Power Formula

```csharp
public class MyFormula : AlphaX.FormulaEngine.Formula
{
    public MyFormula() : base("MyFormula") { }

    public override object Evaluate(params object[] args)
    {
        if (args.Length != 2)
            throw new ArgumentException("MyFormula requires exactly 2 arguments.");

        if (!args.TryGetArgument(0, out double number) ||
            !args.TryGetArgument(1, out double power))
            throw new ArgumentException("Invalid arguments. Expected numbers.");

        return Math.Pow(number, power);
    }

    protected override FormulaInfo GetFormulaInfo()
    {
        FormulaInfo info = new FormulaInfo(Name)
        {
            Description = "Raises a number to the specified power."
        };
        info.AddArgument(new DoubleArgument("number", true));
        info.AddArgument(new DoubleArgument("power", true));
        return info;
    }
}
```

Now integrate it with the engine:

```csharp
AlphaX.FormulaEngine.AlphaXFormulaEngine engine = new AlphaX.FormulaEngine.AlphaXFormulaEngine();
engine.FormulaStore.Add(new MyFormula());
var result = engine.Evaluate("MyFormula(4,3)");
Console.WriteLine(result.Value); // 64
```

---

## Updated Formula Structure Example

```csharp
internal class StartsWithFormula : Formula
{
    public StartsWithFormula() : base("STARTSWITH") { }

    public override object Evaluate(params object[] args)
    {
        ValidateArgumentCount(args);

        if (!args.TryGetArgument(0, out string source))
            throw new ArgumentException("Invalid argument at index 0. Expected a string value.");

        if (!args.TryGetArgument(1, out string value))
            throw new ArgumentException("Invalid argument at index 1. Expected a string value.");

        args.TryGetArgument(2, out bool matchCase);
        return source.StartsWith(value, matchCase ? StringComparison.Ordinal : StringComparison.InvariantCultureIgnoreCase);
    }

    protected override FormulaInfo GetFormulaInfo()
    {
        FormulaInfo info = new FormulaInfo(Name)
        {
            Description = "Checks if the provided string starts with the specified value."
        };
        info.AddArgument(new StringArgument("source", true)
        {
            Description = "The source string."
        });
        info.AddArgument(new StringArgument("value", true)
        {
            Description = "The value to check for."
        });
        info.AddArgument(new BooleanArgument("matchCase", false)
        {
            Description = "Whether to match case."
        });
        return info;
    }
}
```

```csharp
internal class SumFormula : Formula
{
    public SumFormula() : base("SUM") { }

    public override object Evaluate(params object[] args)
    {
        double sum = 0;
        for (int index = 0; index < args.Length; index++)
        {
            if (args.TryGetArgument(index, out double argument))
            {
                sum += argument;
            }
        }
        return sum;
    }

    protected override FormulaInfo GetFormulaInfo()
    {
        FormulaInfo info = new FormulaInfo(Name)
        {
            Description = "Returns the sum of provided values."
        };
        info.AddArgument(new ArrayArgument("values", true)
        {
            Description = "Array of numeric values."
        });
        return info;
    }
}
```

---

## Customizing Engine Settings

### Formula Format

You can change how formulas are written:

```csharp
engine.ApplySettings(new EngineSettings()
{
     OpenBracketSymbol = "[",
     CloseBracketSymbol = "]",
     ArgumentsSeparatorSymbol = "|",
});

var result = engine.Evaluate("MyFormula[4|3]");
Console.WriteLine(result.Value); // 64
```

### Operator Mode

You can use `eq`, `ne`, `lt`, `gt`, etc. instead of `=`, `!=`, `<`, `>`:

```csharp
engine.ApplySettings(new EngineSettings()
{
     LogicalOperatorMode = LogicalOperatorMode.Query
});
```

Supported query operators:
```
=   → eq
!=  → ne
<   → lt
>   → gt
<=  → le
>=  → ge
&&  → and
||  → or
```

### Parse Order

Optimize performance by setting parse order:

```csharp
ParseOrder order = new ParseOrder(ParseType.Number);
order.Add(ParseType.String);
order.Add(ParseType.Boolean);

EngineSettings settings = new EngineSettings();
settings.EngineParseOrder = order;
engine.ApplySettings(settings);
```

---

## Custom Names as Arguments

Use `$CustomName` placeholders resolved at runtime:

```csharp
public class TestEngineContext : IEngineContext
{
    public object Resolve(string key)
    {
        return key switch
        {
            "CustomName1" => 2,
            "CustomName2" => 10,
            "CustomName3" => "TestString",
            _ => throw new Exception("Invalid custom name")
        };
    }
}
```

```csharp
var engine = new AlphaXFormulaEngine(new TestEngineContext());
var result = engine.Evaluate("EQUALS($CustomName1, 2)");
Console.WriteLine(result.Value); // true
```

---

## Nested Formulas

You can use one formula inside another:

```csharp
var result = engine.Evaluate("MyFormula(4, MyFormula(2,2))");
Console.WriteLine(result.Value); // 256
```

---

## Simplifying Expressions

Break complex expressions into smaller parts using **SequencedExpressionBuilder**:

```csharp
var engine = new AlphaXFormulaEngine();

var expression = SequencedExpressionBuilder
    .Create("Result1", "SUM(1,2,12)")
    .Next("Result2", "AVERAGE(1,2,$Result1)")
    .Next("Result3", "SUM(1,2,$Result2)");

var result = engine.Evaluate(expression); // Result: 9
```

That's all of it!

Built by a developer for developers :-)
