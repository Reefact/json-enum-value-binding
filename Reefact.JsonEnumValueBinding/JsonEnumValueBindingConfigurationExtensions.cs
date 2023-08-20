#region Usings declarations

using System;

using Microsoft.Extensions.DependencyInjection;

#endregion

namespace Reefact.JsonEnumValueBinding {

    public static class JsonEnumValueBindingConfigurationExtensions {

        #region Statics members declarations

        public static IMvcBuilder AddJsonEnumValueBinding(this IMvcBuilder builder) {
            if (builder is null) { throw new ArgumentNullException(nameof(builder)); }

            builder.AddMvcOptions(options => options.ModelBinderProviders.Insert(0, new JsonEnumValueBinderProvider()));
            builder.AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonEnumValueConverterFactory()));

            return builder;
        }

        #endregion

    }

}