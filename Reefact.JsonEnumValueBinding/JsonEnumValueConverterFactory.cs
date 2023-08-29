#region Usings declarations

using System;
using System.Text.Json;
using System.Text.Json.Serialization;

#endregion

namespace Reefact.JsonEnumValueBinding {

    /// <summary>
    ///     Supports custom conversion of all enumerations values flagged with the <see cref="JsonEnumValueAttribute" />
    ///     attribute.
    /// </summary>
    public sealed class JsonEnumValueConverterFactory : JsonConverterFactory {

        /// <inheritdoc />
        public override bool CanConvert(Type typeToConvert) {
            if (typeToConvert is null) { throw new ArgumentNullException(nameof(typeToConvert)); }

            return typeToConvert.IsEnum;
        }

        /// <inheritdoc />
        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options) {
            if (typeToConvert is null) { throw new ArgumentNullException(nameof(typeToConvert)); }
            if (options is null) { throw new ArgumentNullException(nameof(options)); }

            Type converterType = typeof(JsonEnumValueConverter<>).MakeGenericType(typeToConvert);

            return (JsonConverter)Activator.CreateInstance(converterType)!;
        }

    }

}