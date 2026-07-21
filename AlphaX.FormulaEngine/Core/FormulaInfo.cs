using System;
using System.Collections.Generic;
using System.Linq;

namespace AlphaX.FormulaEngine
{
    /// <summary>
    /// Describes a formula, including its name, description, arguments, and argument count constraints.
    /// </summary>
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

        /// <summary>
        /// Initializes a new instance of FormulaInfo with the specified formula name.
        /// </summary>
        /// <param name="name">The unique name of the formula.</param>
        public FormulaInfo(string name)
        {
            Name = name;
            _arguments = new List<FormulaArgument>();
            MinArgsCount = 0;
            MaxArgsCount = 0;
        }

        /// <summary>
        /// Adds an argument definition to this formula. Throws if an argument with the same name already exists.
        /// </summary>
        /// <param name="argument">The argument to add.</param>
        public void AddArgument(FormulaArgument argument)
        {
            if (_arguments.Any(x => x.IsVariadic))
            {
                throw new InvalidOperationException("A variadic argument must be the last argument.");
            }

            if (_arguments.Any(x => string.Equals(x.Name, argument.Name, System.StringComparison.InvariantCultureIgnoreCase)))
            {
                throw new InvalidOperationException($"A formula argument with name '{argument.Name}' already exist.");
            }

            _arguments.Add(argument);

            if(argument.Required)
            {
                MinArgsCount++;
            }

            if (argument.IsVariadic)
            {
                MaxArgsCount = int.MaxValue;
            }
            else
            {
                MaxArgsCount = _arguments.Count;
            }
        }

        /// <summary>
        /// Returns the formula signature as a string, e.g. SUM(values:[array]).
        /// </summary>
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
