using DevBrewLabs.Evalis.Resources;
using System;

namespace DevBrewLabs.Evalis
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
        /// Gets whether the formula opts out of automatic error short-circuiting and handles IEvaluationResult errors manually.
        /// </summary>
        public bool HandlesErrors { get; protected set; } = false;

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

        public override string ToString()
        {
            return Info.ToString();
        }
    }
}
