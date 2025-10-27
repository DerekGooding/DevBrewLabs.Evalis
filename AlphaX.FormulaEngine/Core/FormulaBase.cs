using AlphaX.FormulaEngine.Resources;
using System;

namespace AlphaX.FormulaEngine
{
    public abstract class FormulaBase
    {
        /// <summary>
        /// Gets the unique formula name.
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// Gets the formula information.
        /// </summary>
        public FormulaInfo Info { get; }
        /// <summary>
        /// Gets whether the formula is async
        /// </summary>
        public bool IsAsync { get; }

        public FormulaBase(string name, bool isAsync)
        {
            Name = name;
            Info = GetFormulaInfo();

            if (Info == null)
                throw new ArgumentNullException(nameof(Info));
            IsAsync = isAsync;
        }

        protected abstract FormulaInfo GetFormulaInfo();

        protected void ValidateArgumentCount(object[] args)
        {
            if (args == null || args.Length > Info.MaxArgsCount || args.Length < Info.MinArgsCount)
            {
                throw new ArgumentNullException(string.Format(
                    FormulaResources.InvalidArgumentCount,
                    Info.MinArgsCount,
                    Info.MaxArgsCount));
            }
        }

        public override string ToString()
        {
            return Info.ToString();
        }
    }
}
