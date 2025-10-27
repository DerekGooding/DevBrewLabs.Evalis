

# AlphaX.FormulaEngine

A strong and fast library to parse and evaluate formulas. It also supports custom formulas.  
This library is built using [AlphaX.Parserz](https://www.nuget.org/packages/AlphaX.Parserz).

[AlphaX.FormulaEngine Github repo](https://github.com/kartikdeepsagar/AlphaX.FormulaEngine)
[For feedback and queries](https://forms.gle/dfv8E8zpC2qPJS7i7)

---
### ⚠️ **Note: This version (3.0.0) contains breaking changes. See the sections below for details.** 

## Using AlphaXFormulaEngine

You can initialize the engine and evaluate formulas like this:

```csharp
AlphaX.FormulaEngine.AlphaXFormulaEngine engine = new AlphaX.FormulaEngine.AlphaXFormulaEngine();
// Sync evaluation
IEvaluationResult result = engine.Evaluate("SUM(1,2,12.3,5.9099)");
Console.WriteLine(result.Value); // 21.2099
// Async evaluation
IEvaluationResult result = await engine.EvaluateAsync("SUM(1,2,12.3,5.9099)");
Console.WriteLine(result.Value); // 21.2099
```



## Inbuilt Formulas

AlphaX formula engine comes with inbuilt formulas to make it 

Arithmetic Formulas - SUM, AVERAGE, FLOOR, ROUND etc
String Formulas - LOWER, UPPER, TEXTSPLIT, STARTSWITH, ENDSWITH etc.
DateTime Formulas - TODAY, NOW etc.
Logical Formulas - EQUALS, GREATERTHAN, OR, AND etc.
Array Formulas - ARRAYCONTAINS, ARRAYINCLUDES etc.

[Click here to see all inbuilt formulas](https://github.com/kartikdeepsagar/AlphaX.FormulaEngine/blob/master/Formulas.md)

### ⚠️ **Breaking Change (v3.0.0)**  

- Array literals no longer use square brackets.
> 
> Old syntax: `SUM([1, 2, 3]), ARRAYCONTAINS(["Val", "Val2"], "Val2")`  
> New syntax: `SUM(1, 2, 3), ARRAYCONTAINS(ARRAY("Val", "Val2"), "Val2")`  
>
> Update your formulas accordingly before upgrading.  

## Creating your own Formula

This is one of the best feature of AlphaXFormulaEngine. It provides you enough flexibility to write your own formula and easily integrate it with the engine.

1. Create a new MyFormula class which inherits from AlphaX.FormulaEngine.**Formula** class
The below code is for the 'StartsWith' formula which returns if the provided value starts with a specific value.

```csharp
public class MyFormula : AlphaX.FormulaEngine.Formula
{
        public MyFormula() : base("MyFormula")
        {
        }

        public override object Evaluate(params object[] args)
        {
            throw new NotImplementedException();
        }

        protected override FormulaInfo GetFormulaInfo()
        {
            throw new NotImplementedException();
        }
}
```
In the above code, the base() call accepts the name of the formula to be used in formula string.

2. Let's just say our formula will return true/false if a string contains a specific value. So, we'll start by writing the code in the above evaluate method as follows:
```csharp
    public override object Evaluate(IFormulaContext context)
    {
        ValidateArgumentCount(context.Args); // Will throw error if argument count doesn't match.

		string source = context.GetStringArg(0); // Will throw error if the 0th argument is not String
		string value = context.GetStringArg(1);

		context.TryGetArg(2, out bool matchCase); // Will not throw any error
        return source.StartsWith(value, matchCase ? StringComparison.Ordinal : StringComparison.InvariantCultureIgnoreCase);
    }
```
3. We also need to provide some additional metadata for our formula using the GetFormulaInfo method as follows:
```csharp
    protected override FormulaInfo GetFormulaInfo()
    {
        FormulaInfo info = new FormulaInfo(Name)
        {
            Description = "Checks if the provided string starts with the speicifed value."
        };
        // Define arguments for function description. 
        info.AddArgument(new StringArgument("source", true){ Description = "The source string." });
        info.AddArgument(new StringArgument("value", true){ Description = "The value to check for." });
        info.AddArgument(new BooleanArgument("matchCase", false) { Description = "Match case while checking." });
        return info;
    }
}
```
4. Now our formula is ready and the only thing left is to integrate it with the engine by using AlphaXFormulaEngine.FormulaStore's **Add** method as follows:

```csharp
AlphaX.FormulaEngine.AlphaXFormulaEngine engine = new AlphaX.FormulaEngine.AlphaXFormulaEngine();
engine.FormulaStore.Add(new MyFormula());
var result1 = engine.Evaluate("MyFormula(\"This is test\", \"test\")");
Console.WriteLine(result1.Value); // true
var result2 = engine.Evaluate("MyFormula(\"This is test\", \"hello\")");
Console.WriteLine(result2.Value); // false
```
 ### Async Formulas
 AlphaX formula engine now supports Async formulas. To implement async formulas you need to inherit AlphaX.FormulaEngine.**AsyncFormula** and override its **EvaluateAsync** method.

## Customizing Engine Settings

AlphaXFormulaEngine allows you to customize the formula string format. By, default the formula format is :

FormulaName(argument1, argument2, argument3......)

However, you can customize this as per your needs. For example, you can change it to:

FormulaName[argument1 | argument 2 | argument 3....]

Doing this is a piece of cake using engine settings as follows:

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


AlphaXFormulaEngine allows you to choose different type logical operators. For example, You can use 'eq' instead of '=' with logical expression. For example, IF(1 eq 1, true, false)

```c#
_formulaEngine.ApplySettings(new EngineSettings()
{
     LogicalOperatorMode = LogicalOperatorMode.Query
});
```
Below are the current supported operators in Query mode.
```
'=' as 'eq'
'!=' as 'ne'
'<' as 'lt'
'>' as 'gt'
'<=' as 'le'
'>=' as 'ge'
'&&' as 'and'
'||' as 'or'
```

### Parse Order

AlphaXFormulaEngine allows you to optimize the engine performance based on your use case. You can argument parsing order. For example, if your use case requires formulas with only the number arguments then you can specify the parse order as:

```c#
ParseOrder order = new ParseOrder(ParseType.Number); // first try to parse a number
order.Add(ParseType.String) //then try to parse a string.
order.Add(ParseType.Boolean) // then try to parse a boolean i.e. true/false
EngineSettings settings = new EngineSettings();
settings.EngineParseOrder = order;
_formulaEngine.ApplySettings(settings);
```


## Custom Name as Arguments

AlphaXFormulaEngine allows you to use custom names as formula arguments which can be resolved using AlphaX.FormulaEngine.IEngineContext.

For example, we can provide support for the 'CustomName1', ' 'CustomName2, 'CustomName3' custom names as follows:
1. Create an engine context using IEngineContext interface:
```c#
public class TestEngineContext : IEngineContext
{
    public async Task<object> Resolve(string key)
    {
        switch (key)
        {
            case "CustomName1":
                return 2; // return 2 if custom name is 'CustomName1'

            case "CustomName2":
                return 10; // return 10 if custom name is 'CustomName2'

            case "CustomName3":
                return await DoSomething(); // Return data from async task if custom name is 'CustomName3'
        }

        throw new Exception("Invalid custom name");
    }
}
```
2. Now pass the context to the engine as follows:
```c#
 AlphaXFormulaEngine formulaEngine = new AlphaXFormulaEngine(new TestEngineContext());
```
Now since we have create our context. we can simply evaluate it as follows:
```
AlphaX.FormulaEngine.IEvaluationResult result = engine.Evaluate("EQUALS($CustomName1, 2)");
Console.WriteLine(result.Value);  // true
```
Note: Custom names should start with a dollar ($) symbol and it will be passed to the IEngineContext without dollar sign during evaluation.

### ⚠️ **Breaking Change (v3.0.0)**  

IEngineContext now returns a `Task<object>`  instead of `object` to support async resolution of custom names.
> Old - `object` Resolve(string key)
> New - `Task<object>` Resolve(string key)
> 
> Update your implementations of IEngineContext if any

## Nested Formulas

To make your life easy, we have also added support for nested formulas. So, you can use a formula as a formula argument for another formula as follows:
```c#
AlphaX.FormulaEngine.IEvaluationResult result = engine.Evaluate("MyFormula(4, MyFormula(2,2))");
Console.WriteLine(result.Value); // 256
```

## Simplifying Large Expressions

Sometimes expressions can be headache when they become too large. Don't worry AlphaX's FormulaEngine solves this for you by allowing you to break down expressions into smaller segments and evaluate them.

Let's understand with an example expression i.e. "*SUM(1,2,AVERAGE(1,2,SUM(1,2,12)))*". 

Though it isn't that much complex but still it could make your head spin when it comes to troubleshoot them. 

Let's break it down!

You can do it using the *AlphaX.FormulaEngine.**SequencedExpressionBuilder***. 
```c#
var engine = new AlphaXFormulaEngine();
var expression = SequencedExpressionBuilder
    .Create("Result1", "SUM(1,2,12)")
    .Next("Result2", "AVERAGE(1,2,$Result1)")
    .Next("Result3", "SUM(1,2,$Result2)");
var result = engine.Evaluate(expression); // Result: 9
```
1. First "*SUM(1,2,12)*" will be evaluated and it's result will be stored in a variable named "Result1".
2. Now you can use that result variable as custom name in the next part of the expression as "*AVERAGE(1,2,$Result1)*" and store the result in another variable named "Result2".
3. Now the "Result2" variable will be used in the next expression "*SUM(1,2,$Result2)*".
4. And you can keep on doing it until you feel satisfied with your broken-down expression.

That's all of it :-)

Built by developer for developers :-)