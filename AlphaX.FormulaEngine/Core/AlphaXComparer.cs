using System;

namespace AlphaX.FormulaEngine
{
    internal static class AlphaXComparer
    {
        public static bool Compare(object left, string @operator, object right, LogicalOperators supportedOperators)
        {
            try
            {
                if (@operator == supportedOperators.EqualsTo)
                    return Equals(left, right);

                if (@operator == supportedOperators.NotEquals)
                    return !Equals(left, right);

                if (@operator == supportedOperators.AND)
                {
                    return (bool)left && (bool)right;
                }

                if (@operator == supportedOperators.OR)
                {
                    return (bool)left || (bool)right;
                }

                if (@operator == supportedOperators.LessThan)
                {
                    if (left is double num1 && right is double num2)
                    {
                        return num1 < num2;
                    }
                    else if (left is DateTime date1 && right is DateTime date2)
                    {
                        return date1 < date2;
                    }
                }

                if (@operator == supportedOperators.LessThanEqualsTo)
                {
                    if (left is double num1 && right is double num2)
                    {
                        return num1 <= num2;
                    }
                    else if (left is DateTime date1 && right is DateTime date2)
                    {
                        return date1 <= date2;
                    }
                }

                if (@operator == supportedOperators.GreaterThan)
                {
                    if (left is double num1 && right is double num2)
                    {
                        return num1 > num2;
                    }
                    else if (left is DateTime date1 && right is DateTime date2)
                    {
                        return date1 > date2;
                    }
                }

                if (@operator == supportedOperators.GreaterThanEqualsTo)
                {
                    if (left is double num1 && right is double num2)
                    {
                        return num1 >= num2;
                    }
                    else if (left is DateTime date1 && right is DateTime date2)
                    {
                        return date1 >= date2;
                    }
                }

                throw new Exception();
            }
            catch
            {
                throw new EvaluationException($"Invalid operator used with operands. {left} {@operator} {right}");
            }
        }
    }
}