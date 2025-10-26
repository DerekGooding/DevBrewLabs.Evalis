using System;

namespace AlphaX.FormulaEngine.Utils
{
    internal static class Extensions
    {
        public static bool TryGetArgument<T>(this object[] array, int index, out T value)
        {
            if (index < array.Length && array[index] is T t)
            {
                value = t;
                return true;
            }

            value = default;
            return false;
        }
    }
}
