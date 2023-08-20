#region Usings declarations

using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc.ModelBinding;

#endregion

namespace Reefact.JsonEnumValueBinding {

    internal sealed class JsonEnumValueBinder : IModelBinder {

        /// <inheritdoc />
        public Task BindModelAsync(ModelBindingContext bindingContext) {
            if (bindingContext is null) { throw new ArgumentNullException(nameof(bindingContext)); }

            string              modelName           = bindingContext.ModelName;
            ValueProviderResult valueProviderResult = bindingContext.ValueProvider.GetValue(modelName);
            if (valueProviderResult == ValueProviderResult.None) { return Task.CompletedTask; }

            string? value    = valueProviderResult.FirstValue;
            Type    enumType = bindingContext.ModelType;

            if (Enum.TryParse(enumType, value, true, out object? enumValue)) {
                bindingContext.Result = ModelBindingResult.Success(enumValue);
            } else {
                FieldInfo? field = enumType.GetFields(BindingFlags.Public | BindingFlags.Static).FirstOrDefault(f => f.GetCustomAttribute<JsonEnumValueAttribute>()?.Value == value);
                if (field != null) {
                    object? enumFieldValue = field.GetValue(null);
                    bindingContext.Result = ModelBindingResult.Success(enumFieldValue);
                } else {
                    bindingContext.Result = ModelBindingResult.Failed();
                }
            }

            return Task.CompletedTask;
        }

    }

}