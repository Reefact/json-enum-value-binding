#region Usings declarations

using System;
using System.Diagnostics.CodeAnalysis;

#endregion

namespace Reefact.JsonEnumValueBinding {

    /// <summary>
    ///     Specifies the enum value that is present in the JSON when serializing and deserializing.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "UnusedType.Global")]
    public sealed class JsonEnumValueAttribute : Attribute {

        #region Constructors declarations

        /// <summary>
        ///     Initialize a new instance of <see cref="JsonEnumValueAttribute" /> with the specified enum value.
        /// </summary>
        /// <param name="value">The JSON value of the enum.</param>
        /// <exception cref="ArgumentNullException">if <paramref name="value" /> is null.</exception>
        /// <exception cref="ArgumentException">if <paramref name="value" /> is empty or whitespace.</exception>
        public JsonEnumValueAttribute(string value) {
            if (value is null) { throw new ArgumentNullException(nameof(value)); }
            if (string.IsNullOrWhiteSpace(value)) { throw new ArgumentException("Value cannot be null or whitespace.", nameof(value)); }

            Value = value;
        }

        #endregion

        /// <summary>
        ///     Gets the value of the enum.
        /// </summary>
        /// <returns>The name of the enum.</returns>
        public string Value { get; }

    }

}