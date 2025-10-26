using AlphaX.FormulaEngine.Resources;
using System;

namespace AlphaX.FormulaEngine
{
    public abstract class Formula
    {
        /// <summary>
        /// Gets the unique formula name.
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// Gets the formula information.
        /// </summary>
        public FormulaInfo Info { get; }

        public Formula(string name)
        {
            Name = name;
            Info = GetFormulaInfo();

            if(Info == null)
                throw new ArgumentNullException(nameof(Info));
        }

        /// <summary>
        /// Gets the evaluated result.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public abstract object Evaluate(IFormulaContext context);

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
