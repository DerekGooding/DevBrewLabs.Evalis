using AlphaX.FormulaEngine.Resources;
using System;

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

        public void Dispose()
        {
            Evaluator = null;
        }
    }
}
