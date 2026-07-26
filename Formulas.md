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
- **ABS** – Rounds absolute value of the provided number.  
  Example: `ABS(1.4)` → 1.5 

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
  Example: `LENGTH("Evalis")` → 6  
- **CONTAINS** – Checks if string contains another string.  
  Example: `CONTAINS("Evalis", "pha")` → true  
- **STARTSWITH** – Checks if string starts with given string.  
  Example: `STARTSWITH("Evalis", "Al", true)` → true  
- **ENDSWITH** – Checks if string ends with given string.  
  Example: `ENDSWITH("Evalis", "ax")` → true  
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
  Example: `IF(UPPER("Evalis") = UPPER("Evalis"), true, false)` → true  

### Array Formulas

- **ARRAYCONTAINS** – Checks if array contains a value.  
  Example: `ARRAYCONTAINS([1,2,3], 2)` → true  
- **ARRAYINCLUDES** – Checks if array includes all values.  
  Example: `ARRAYINCLUDES([1,2,3,4], [3,4])` → true  
- **ARRAY** - Returns the argument values as an array
   Example: `ARRAY(1,2,3,4)` → [1,2,3,4]
> **Note:** More formulas will be added in future updates.