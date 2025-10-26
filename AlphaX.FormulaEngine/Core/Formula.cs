using System;

namespace AlphaX.FormulaEngine
{
    public abstract class Formula
    {
        internal AlphaXFormulaEngine Engine { get; set; }

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
        /// <param name="args"></param>
        /// <returns></returns>
        public abstract object Evaluate(params object[] args);

        protected void ValidateArgumentCount(object[] args)
        {
            if(args == null || args.Length > Info.MaxArgsCount)
            {
                throw new ArgumentNullException("Invalid arguments count.");
            } 
        }

        protected abstract FormulaInfo GetFormulaInfo();

        public override string ToString()
        {
            return Info.ToString();
        }
    }
}
