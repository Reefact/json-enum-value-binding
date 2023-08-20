#region Usings declarations

using System;

using Microsoft.AspNetCore.Mvc.ModelBinding;

#endregion

namespace Reefact.JsonEnumValueBinding {

    internal sealed class JsonEnumValueBinderProvider : IModelBinderProvider {

        /// <inheritdoc />
        public IModelBinder? GetBinder(ModelBinderProviderContext context) {
            if (context is null) { throw new ArgumentNullException(nameof(context)); }

            if (context.Metadata.ModelType.IsEnum) { return new JsonEnumValueBinder(); }

            return null;
        }

    }

}