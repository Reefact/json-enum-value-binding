#region Usings declarations

using System;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

#endregion

namespace Reefact.JsonEnumValueBinding {

    internal sealed class JsonEnumValueConverter<TEnum> : JsonConverter<TEnum>
        where TEnum : struct, Enum {

        #region Fields declarations

        private readonly Type _enumType = typeof(TEnum);

        #endregion

        /// <inheritdoc />
        public override TEnum Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
            if (typeToConvert is null) { throw new ArgumentNullException(nameof(typeToConvert)); }
            if (options is null) { throw new ArgumentNullException(nameof(options)); }

            if (reader.TokenType != JsonTokenType.String) { throw new JsonException($"Cannot convert '{reader.TokenType}' to '{_enumType.Name}'."); }

            string? enumAsString = reader.GetString();
            foreach (TEnum value in Enum.GetValues(_enumType)) {
                FieldInfo? field = _enumType.GetField(value.ToString());
                if (field == null) { continue; }

                if (field.GetCustomAttributes(typeof(JsonEnumValueAttribute), true).FirstOrDefault() is JsonEnumValueAttribute attribute
                 && attribute.Value == enumAsString) {
                    return value;
                }
            }

            throw new JsonException($"Cannot convert '{enumAsString}' to '{_enumType.Name}'.");
        }

        /// <inheritdoc />
        public override void Write(Utf8JsonWriter writer, TEnum value, JsonSerializerOptions options) {
            if (writer is null) { throw new ArgumentNullException(nameof(writer)); }
            if (options is null) { throw new ArgumentNullException(nameof(options)); }

            string     name  = value.ToString();
            FieldInfo? field = _enumType.GetField(name);
            if (field == null) { throw new JsonException($"Value '{name}' does not represents a valid '{_enumType.Name}'."); }

            if (field.GetCustomAttributes(typeof(JsonEnumValueAttribute), true).FirstOrDefault() is JsonEnumValueAttribute attribute) {
                writer.WriteStringValue(attribute.Value);
            } else {
                writer.WriteStringValue(name);
            }
        }

    }

}