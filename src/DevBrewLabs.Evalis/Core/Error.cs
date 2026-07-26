using System;

namespace DevBrewLabs.Evalis
{
    /// <summary>
    /// Represents the type of error. This is a strongly typed wrapper to allow custom error codes.
    /// </summary>
    public readonly struct Error : IEquatable<Error>
    {
        /// <summary>
        /// Gets the string code of the error type.
        /// </summary>
        public string Code { get; }
        /// <summary>
        /// Gets the error message.
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Error"/> struct.
        /// </summary>
        /// <param name="code">The error code.</param>
        /// <param name="message">The error message.</param>
        public Error(string code, string message = null)
        {
            Code = code;
            Message = message;
        }

        public static readonly Error DivideByZero = new Error(ErrorCode.DivideByZero, "Can't divide by zero.");
        public static Error General(string message) => new Error(ErrorCode.General, message);
        public static Error Syntax(string message) => new Error(ErrorCode.Syntax, message);
        public static Error Name(string message) => new Error(ErrorCode.Name, message);
        public static Error Value(string message) => new Error(ErrorCode.Value, message);

        public bool Equals(Error other) => string.Equals(Code, other.Code, StringComparison.OrdinalIgnoreCase);

        public override bool Equals(object obj) => obj is Error other && Equals(other);

        public override int GetHashCode() => Code?.ToUpperInvariant().GetHashCode() ?? 0;

        public static bool operator ==(Error left, Error right) => left.Equals(right);

        public static bool operator !=(Error left, Error right) => !left.Equals(right);

        public static implicit operator string(Error error) => error.Message;

        public override string ToString() => Message ?? Code ?? string.Empty;
    }

    public class ErrorCode
    {
        public const string General = "General";
        public const string Syntax = "Syntax";
        public const string Value = "Value";
        public const string DivideByZero = "DivideByZero";
        public const string Name = "Name";
        public const string Num = "Num";
        public const string NA = "NA";
        public const string Null = "Null";
    }
}
