#region Usings declarations

using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

using Microsoft.Extensions.DependencyInjection;

#endregion

namespace Reefact.JsonEnumValueBinding {

    /// <summary></summary>
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [SuppressMessage("ReSharper", "UnusedType.Global")]
    public static class JsonEnumValueBindingConfigurationExtensions {

        #region Statics members declarations

        /// <summary>
        ///     Enables support for serialization and binding of custom enumeration values.
        /// </summary>
        /// <param name="builder">The builder for configuring MVC service.</param>
        /// <returns>The builder for configuring MVC service</returns>
        /// <exception cref="ArgumentNullException">if <paramref name="builder" /> is <c>null</c>.</exception>
        public static IMvcBuilder AddJsonEnumValueBinding(this IMvcBuilder builder) {
            if (builder is null) { throw new ArgumentNullException(nameof(builder)); }

            builder.AddMvcOptions(options => options.ModelBinderProviders.Insert(0, new JsonEnumValueBinderProvider()));
            builder.AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonEnumValueConverterFactory()));

            return builder;
        }

        /// <summary>
        ///     Enables support for serialization and binding of custom enumeration values.
        /// </summary>
        /// <param name="builder">The builder for configuring MVC service.</param>
        /// <param name="configure">Configures an external serializer instance.</param>
        /// <returns>The builder for configuring MVC service</returns>
        /// <exception cref="ArgumentNullException">if <paramref name="builder" /> is <c>null</c>.</exception>
        public static IMvcBuilder AddJsonEnumValueBinding(this IMvcBuilder builder, Action<Action<JsonSerializerOptions>> configure) {
            if (builder is null) { throw new ArgumentNullException(nameof(builder)); }
            if (configure is null) { throw new ArgumentNullException(nameof(configure)); }

            AddJsonEnumValueBinding(builder);
            configure(options => options.Converters.Add(new JsonEnumValueConverterFactory()));

            return builder;
        }

        #endregion

    }

}