using System;

namespace AlphaX.FormulaEngine.Utils
{
    internal static class Extensions
    {
        public static T GetValueOrDefault<T>(this object[] array, int index, T defaultValue)
        {
            return index >= 0 && index < array.Length ? (T)array[index] : defaultValue;
        }
    }
}
