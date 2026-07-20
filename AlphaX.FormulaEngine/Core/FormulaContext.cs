using AlphaX.FormulaEngine.Resources;
using System;
using System.Collections.Generic;

namespace AlphaX.FormulaEngine
{
    public interface IFormulaContext
    {
        object[] Args { get; }
        object[] GetArrayArg(int index);
        bool GetBooleanArg(int index);
        double GetDoubleArg(int index);
        object GetObjectArg(int index);
        string GetStringArg(int index);
        bool TryGetArg<T>(int index, out T arg);
        /// <summary>
        /// Flattens all arguments (including nested arrays) into a sequence of <typeparamref name="T"/>.
        /// Use this for variadic aggregator formulas (SUM, MAX, MIN, AVERAGE) that must handle
        /// both scalar values and range/array arguments transparently.
        /// </summary>
        IEnumerable<T> GetFlattenedArgs<T>();
    }

    internal class FormulaContext : IFormulaContext, IDisposable
    {
        public object[] Args { get; }
        internal Evaluator Evaluator { get; set; }

        internal FormulaContext(object[] args)
        {
            Args = args;
        }

        public string GetStringArg(int index)
        {
            if(!TryGetArg(index, out string arg))
            {
                throw new ArgumentException(string.Format(FormulaResources.InvalidStringArgument, index));
            }

            return arg;
        }

        public double GetDoubleArg(int index)
        {
            if (!TryGetArg(index, out double arg))
            {
                throw new ArgumentException(string.Format(FormulaResources.InvalidDecimalArgument, index));
            }

            return arg;
        }

        public bool GetBooleanArg(int index)
        {
            if (!TryGetArg(index, out bool arg))
            {
                throw new ArgumentException(string.Format(FormulaResources.InvalidBooleanArgument, index));
            }

            return arg;
        }

        public object GetObjectArg(int index)
        {
            if (!TryGetArg(index, out object arg))
            {
                throw new ArgumentException(string.Format(FormulaResources.InvalidObjectArgument, index));
            }

            return arg;
        }

        public object[] GetArrayArg(int index)
        {
            if (!TryGetArg(index, out object[] arg))
            {
                throw new ArgumentException(string.Format(FormulaResources.InvalidArrayArgument, index));
            }

            return arg;
        }

        public bool TryGetArg<T>(int index, out T arg)
        {
            if (index < Args.Length && Args[index] is T t)
            {
                arg = t;
                return true;
            }

            arg = default;
            return false;
        }

        public IEnumerable<T> GetFlattenedArgs<T>()
        {
            foreach (var arg in Args)
            {
                foreach (var value in FlattenValue<T>(arg))
                    yield return value;
            }
        }

        private static IEnumerable<T> FlattenValue<T>(object value)
        {
            if (value is object[] arr)
            {
                foreach (var item in arr)
                {
                    foreach (var nested in FlattenValue<T>(item))
                        yield return nested;
                }
            }
            else if (value is T typed)
            {
                yield return typed;
            }
        }

        public void Dispose()
        {
            Evaluator = null;
        }
    }
}
