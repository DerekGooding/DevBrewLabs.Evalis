using AlphaX.FormulaEngine.Resources;
using System;

namespace AlphaX.FormulaEngine
{
    /// <summary>
    /// Abstract base class for all formula implementations, providing shared metadata and argument count validation.
    /// </summary>
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

        /// <summary>
        /// Initializes a new FormulaBase.
        /// </summary>
        /// <param name="name">The unique name of the formula.</param>
        /// <param name="isAsync">Whether this formula performs async evaluation.</param>
        public FormulaBase(string name, bool isAsync)
        {
            Name = name;
            Info = GetFormulaInfo();

            if (Info == null)
                throw new ArgumentNullException(nameof(Info));
            IsAsync = isAsync;
        }

        /// <summary>
        /// Provides the FormulaInfo metadata for this formula, including argument definitions.
        /// </summary>
        protected abstract FormulaInfo GetFormulaInfo();

        /// <summary>
        /// Validates that the provided argument count falls within the range defined by FormulaInfo.
        /// </summary>
        /// <param name="args">The arguments passed to the formula.</param>
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
