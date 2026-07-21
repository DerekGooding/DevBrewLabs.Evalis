# AlphaX.FormulaEngine Formulas Reference

This document provides a comprehensive list of all supported formulas in AlphaX.FormulaEngine, categorized by their domain.

## 🧮 Arithmetic Formulas

| Formula | Description | Example | Result |
|---------|-------------|---------|--------|
| `SUM(v1, v2...)` | Returns the sum of provided values. | `SUM(1,2,4)` | `7` |
| `AVERAGE(v1...)` | Returns the average of provided values. | `AVERAGE(3,2,4)` | `3` |
| `CEILING(val)` | Returns the smallest integer ≥ specified number. | `CEILING(1.34)` | `2` |
| `FLOOR(val)` | Returns the largest integer ≤ specified number. | `FLOOR(1.34)` | `1` |
| `ROUND(val, [d])`| Rounds to a specified number of decimal places. | `ROUND(1.342, 2)` | `1.34` |
| `ABS(val)` | Returns the absolute value of the provided number. | `ABS(-1.4)` | `1.4` |
| `MIN(v1, v2...)` | Returns the minimum value. | `MIN(10, 5, 20)` | `5` |
| `MAX(v1, v2...)` | Returns the maximum value. | `MAX(10, 5, 20)` | `20` |
| `POWER(base, exp)`| Raises a base to an exponent. | `POWER(2, 3)` | `8` |
| `SQRT(val)` | Returns the square root of a value. | `SQRT(16)` | `4` |
| `MOD(num, div)` | Returns the remainder of division. | `MOD(5, 2)` | `1` |
| `TRUNC(val)` | Truncates a number to an integer. | `TRUNC(1.9)` | `1` |
| `SIGN(val)` | Returns the sign of a number (1, 0, -1). | `SIGN(-5)` | `-1` |
| `LOG(val, [base])`| Returns the logarithm of a number. | `LOG(8, 2)` | `3` |
| `LOG10(val)` | Returns the base-10 logarithm. | `LOG10(100)` | `2` |
| `EXP(val)` | Returns e raised to the power of value. | `EXP(1)` | `2.718...` |
| `PI()` | Returns the value of PI. | `PI()` | `3.141...` |

## 🔤 String Formulas

| Formula | Description | Example | Result |
|---------|-------------|---------|--------|
| `LOWER(str)` | Converts string to lowercase. | `LOWER("TEST")` | `"test"` |
| `UPPER(str)` | Converts string to uppercase. | `UPPER("test")` | `"TEST"` |
| `TEXTSPLIT(sep, str)`| Splits string by separator into an array. | `TEXTSPLIT(".", "John.Doe")`| `["John", "Doe"]` |
| `CONCAT(s1, s2...)`| Joins multiple strings. | `CONCAT("A","B")` | `"AB"` |
| `LENGTH(str)` | Gets string length. | `LENGTH("AlphaX")` | `6` |
| `CONTAINS(str, sub)`| Checks if string contains another string. | `CONTAINS("AlphaX", "pha")` | `true` |
| `STARTSWITH(s, sub, [ignCase])`| Checks if string starts with given string. | `STARTSWITH("AlphaX", "al", true)` | `true` |
| `ENDSWITH(s, sub, [ignCase])`| Checks if string ends with given string. | `ENDSWITH("AlphaX", "ax", true)` | `true` |
| `REPLACE(s, old, new, [ignCase])`| Replaces substring with another. | `REPLACE("a b", "a", "c")` | `"c b"` |
| `TRIM(str)` | Strips leading and trailing whitespaces. | `TRIM(" test ")` | `"test"` |
| `SUBSTRING(str, st, [len])`| Returns a substring. | `SUBSTRING("hello", 1, 3)` | `"ell"` |
| `INDEXOF(str, search)`| Returns the 0-based index of a search string. | `INDEXOF("hello", "e")` | `1` |
| `LEFT(str, len)` | Returns the leftmost characters. | `LEFT("AlphaX", 3)` | `"Alp"` |
| `RIGHT(str, len)` | Returns the rightmost characters. | `RIGHT("AlphaX", 1)` | `"X"` |
| `MID(str, st, len)`| Returns a substring from the middle. | `MID("AlphaX", 1, 3)` | `"lph"` |
| `PAD(str, len, [c])`| Pads a string to a specified length. | `PAD("1", 3, "0")` | `"001"` |
| `REPEAT(str, n)` | Repeats a string n times. | `REPEAT("A", 3)` | `"AAA"` |
| `PROPER(str)` | Capitalizes the first letter of each word. | `PROPER("john doe")` | `"John Doe"` |
| `REGEXMATCH(pat, str)`| Checks if string matches regular expression. | `REGEXMATCH("^a", "apple")`| `true` |
| `FORMAT(str, args)`| Formats a string using placeholders. | `FORMAT("{0} {1}", "A", "B")`| `"A B"` |
| `ISEMPTY(str)` | Checks if a string is empty. | `ISEMPTY("")` | `true` |
| `ISNULLOREMPTY(s)`| Checks if string is null or empty. | `ISNULLOREMPTY(null)` | `true` |

## 📅 DateTime Formulas

| Formula | Description | Example | Result |
|---------|-------------|---------|--------|
| `TODAY()` | Returns system date. | `TODAY()` | `28-04-2023` |
| `NOW()` | Returns system date and time. | `NOW()` | `28-04-2023 10:52:53` |
| `DATETIME(str, [fmt])` | Parses a datetime string. | `DATETIME("2024/01/01", "yyyy/MM/dd")`| `01-01-2024` |
| `YEAR(date)` | Parses the year from a date string. | `YEAR("2023-10-15")` | `2023` |
| `MONTH(date)` | Parses the month from a date string. | `MONTH("2023-10-15")` | `10` |
| `DAY(date)` | Parses the day from a date string. | `DAY("2023-10-15")` | `15` |

## 🧠 Logical Formulas

| Formula | Description | Example | Result |
|---------|-------------|---------|--------|
| `EQUALS(v1, v2)` | Checks equality. | `EQUALS(true, 1 > 3)` | `false` |
| `GREATERTHAN(v1, v2)`| Checks if first value is greater. | `GREATERTHAN(5,2)` | `true` |
| `LESSTHAN(v1, v2)` | Checks if first value is less. | `LESSTHAN(5,2)` | `false` |
| `NOT(bool)` | Negates a boolean value. | `NOT(1 == 1)` | `false` |
| `AND(b1, b2...)` | Logical AND. | `AND(true, 1 != 1)` | `false` |
| `OR(b1, b2...)` | Logical OR. | `OR(true, 1 != 1)` | `true` |
| `IF(cond, trueVal, falseVal)`| Conditional logic. | `IF(1 > 0, true, false)` | `true` |
| `COALESCE(v1, v2...)`| Returns the first non-null value. | `COALESCE(null, "first")`| `"first"` |
| `ISNUMBER(val)` | Returns true if the type is a number. | `ISNUMBER(10)` | `true` |
| `ISSTRING(val)` | Returns true if the type is a string. | `ISSTRING("abc")` | `true` |
| `IFS(c1, v1...)` | Checks multiple conditions in sequence. | `IFS(1>2, "A", 1<2, "B")` | `"B"` |
| `SWITCH(val, c1...)`| Evaluates value against a list of cases. | `SWITCH(2, 1, "A", 2, "B")` | `"B"` |
| `IFERROR(val, alt)`| Returns alternative if value is an error. | `IFERROR(1/0, "Error")` | `"Error"` |
| `IFBLANK(val, alt)`| Returns alternative if value is blank/null. | `IFBLANK(null, "Blank")` | `"Blank"` |
| `ISBOOL(val)` | Checks if the value is a boolean. | `ISBOOL(true)` | `true` |
| `ISDATE(val)` | Checks if the value is a valid date. | `ISDATE("2023/10/10")` | `true` |
| `ISARRAY(val)` | Checks if the value is an array. | `ISARRAY(ARRAY(1, 2))`| `true` |
| `ISNULL(val)` | Checks if the value is null. | `ISNULL(null)` | `true` |

## 📦 Array Formulas

| Formula | Description | Example | Result |
|---------|-------------|---------|--------|
| `ARRAYCONTAINS(arr, val)`| Checks if array contains a value. | `ARRAYCONTAINS(ARRAY(1,2), 2)`| `true` |
| `ARRAYINCLUDES(arr, sub)`| Checks if array includes all values. | `ARRAYINCLUDES(ARRAY(1,2), ARRAY(2))`| `true` |
| `ARRAY(v1, v2...)` | Returns the argument values as an array. | `ARRAY(1,2,3,4)` | `[1, 2, 3, 4]` |
| `INDEX(arr, idx)` | Returns an element from an array by index. | `INDEX(ARRAY(1, 2, 3), 1)`| `2` |
| `JOIN(sep, arr)` | Joins an array into a string. | `JOIN("-", ARRAY("A", "B"))`| `"A-B"` |
| `COUNT(arr)` | Counts the elements in an array. | `COUNT(ARRAY(1, 2, 3))` | `3` |

> **Note:** AlphaX.FormulaEngine allows nested expressions and arithmetic combinations natively! Try evaluating expressions like `(SUM(1,2) + 5) * 10`. You can also configure string quote rules (double quotes vs single quotes) via `EngineSettings.DoubleQuotedStrings`.

## 🎯 Using Variables in Formulas

You can inject standard variables using the `$` prefix, or define your own custom variables (e.g. `[Field]`, `A1`) by passing `CustomTokenParsers` in `EngineSettings`. Variables seamlessly pass into any formula!

| Feature | Example | Description |
|---------|---------|-------------|
| **Standard Variables** | `SUM($Tax, $Subtotal)` | Injects variables from `IEngineContext`. |
| **Custom Variables** | `AVERAGE(A1:B10, [Discount])` | Requires `CustomTokenParsers` to natively parse. |
| **AST Extraction** | `engine.ExtractVariables(formula)` | Statically extracts all variables without evaluating. |