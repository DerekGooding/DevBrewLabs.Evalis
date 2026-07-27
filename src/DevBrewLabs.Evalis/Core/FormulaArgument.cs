using System;

namespace DevBrewLabs.Evalis
{
    /// <summary>
    /// Base class for all formula argument definitions, describing the name, expected type, and whether the argument is required.
    /// </summary>
    public abstract class FormulaArgument
    {
        /// <summary>
        /// Gets or sets the description of the argument.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets the name of the argument.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the type of the argument.
        /// </summary>
        public Type Type { get; }

        /// <summary>
        /// Gets if the argument is required.
        /// </summary>
        public bool Required { get; }

        /// <summary>
        /// Gets if this argument can accept a variable number of parameters (like params in C#).
        /// </summary>
        public bool IsVariadic { get; }

        /// <summary>
        /// Initializes a new FormulaArgument.
        /// </summary>
        /// <param name="name">The argument name.</param>
        /// <param name="type">The expected CLR type for this argument.</param>
        /// <param name="required">Whether this argument must be provided.</param>
        /// <param name="isVariadic">Whether this argument can accept multiple values.</param>
        protected FormulaArgument(string name, Type type, bool required, bool isVariadic = false)
        {
            Name = name;
            Type = type;
            Required = required;
            IsVariadic = isVariadic;
        }

        public override string ToString() => Type.IsArray ? $"{Name}:[array]" : $"{Name}:{Type.Name.ToLower()}";
    }

    /// <summary>
    /// A formula argument that expects a double (numeric) value.
    /// </summary>
    public class DoubleArgument : FormulaArgument
    {
        public DoubleArgument(string name, bool required, bool isVariadic = false) : base(name, typeof(double), required, isVariadic)
        {
        }
    }

    /// <summary>
    /// A formula argument that accepts any object value.
    /// </summary>
    public class ObjectArgument : FormulaArgument
    {
        public ObjectArgument(string name, bool required, bool isVariadic = false) : base(name, typeof(object), required, isVariadic)
        {
        }
    }

    /// <summary>
    /// A formula argument that expects a string value.
    /// </summary>
    public class StringArgument : FormulaArgument
    {
        public StringArgument(string name, bool required, bool isVariadic = false) : base(name, typeof(string), required, isVariadic)
        {
        }
    }

    /// <summary>
    /// A formula argument that expects a boolean value.
    /// </summary>
    public class BooleanArgument : FormulaArgument
    {
        public BooleanArgument(string name, bool required, bool isVariadic = false) : base(name, typeof(bool), required, isVariadic)
        {
        }
    }

    /// <summary>
    /// A formula argument that expects an array (object[]) of values, such as a cell range.
    /// </summary>
    public class ArrayArgument : FormulaArgument
    {
        public ArrayArgument(string name, bool required, bool isVariadic = false) : base(name, typeof(object[]), required, isVariadic)
        {
        }
    }
}