using System;

namespace AlphaX.FormulaEngine
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
        /// Initializes a new FormulaArgument.
        /// </summary>
        /// <param name="name">The argument name.</param>
        /// <param name="type">The expected CLR type for this argument.</param>
        /// <param name="required">Whether this argument must be provided.</param>
        public FormulaArgument(string name, Type type, bool required)
        {
            Name = name;
            Type = type;
            Required = required;
        }

        public override string ToString()
        {
            return Type.IsArray ? $"{Name}:[array]" : $"{Name}:{Type.Name.ToLower()}";
        }
    }

    /// <summary>
    /// A formula argument that expects a double (numeric) value.
    /// </summary>
    public class DoubleArgument : FormulaArgument
    {
        public DoubleArgument(string name, bool required) : base(name, typeof(double), required)
        {
            
        }
    }

    /// <summary>
    /// A formula argument that accepts any object value.
    /// </summary>
    public class ObjectArgument : FormulaArgument
    {
        public ObjectArgument(string name, bool required) : base(name, typeof(object), required)
        {

        }
    }

    /// <summary>
    /// A formula argument that expects a string value.
    /// </summary>
    public class StringArgument : FormulaArgument
    {
        public StringArgument(string name, bool required) : base(name, typeof(string), required)
        {

        }
    }

    /// <summary>
    /// A formula argument that expects a boolean value.
    /// </summary>
    public class BooleanArgument : FormulaArgument
    {
        public BooleanArgument(string name, bool required) : base(name, typeof(bool), required)
        {

        }
    }

    /// <summary>
    /// A formula argument that expects an array (object[]) of values, such as a cell range.
    /// </summary>
    public class ArrayArgument : FormulaArgument
    {
        public ArrayArgument(string name, bool required) : base(name, typeof(object[]), required)
        {

        }
    }
}
