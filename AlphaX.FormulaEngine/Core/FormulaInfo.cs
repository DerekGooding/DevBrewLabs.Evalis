using System;
using System.Collections.Generic;
using System.Linq;

namespace AlphaX.FormulaEngine
{
    public class FormulaInfo
    {
        private List<FormulaArgument> _arguments;

        /// <summary>
        /// Gets the formula name.
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// Gets or sets the description of the formula.
        /// </summary>
        public string Description {  get; set; }
        /// <summary>
        /// Gets the formula arguments.
        /// </summary>
        public IReadOnlyList<FormulaArgument> Arguments => _arguments;
        /// <summary>
        /// Gets minimum number of argument that this formula accepts.
        /// </summary>
        public int MinArgsCount { get; private set; }
        /// <summary>
        /// Gets maximum number of argument that this formula accepts.
        /// </summary>
        public int MaxArgsCount { get; private set; }

        public FormulaInfo(string name)
        {
            Name = name;
            _arguments = new List<FormulaArgument>();
            MinArgsCount = 0;
            MaxArgsCount = 0;
        }

        public void AddArgument(FormulaArgument argument)
        {
            if (_arguments.Any(x => string.Equals(x.Name, argument.Name, System.StringComparison.InvariantCultureIgnoreCase)))
            {
                throw new InvalidOperationException($"A formula argument with name '{argument.Name}' already exist.");
            }

            _arguments.Add(argument);

            if(argument.Required)
            {
                MinArgsCount++;
            }

            MaxArgsCount = _arguments.Count;
        }

        public override string ToString()
        {
            if (Arguments.Any())
            {
                return $"{Name}({string.Join(",", Arguments.Select(x => x.ToString()))})";
            }

            return $"{Name}()";
        }
    }
}
