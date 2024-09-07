using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ConwayGameOfLife.API.Binders
{
    public class InitialStateModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.FieldName);
            if (valueProviderResult == ValueProviderResult.None)
            {
                bindingContext.ModelState.AddModelError(bindingContext.FieldName, "The initialState field is required.");
                return Task.CompletedTask;
            }

            var initialState = valueProviderResult.FirstValue;

            if (string.IsNullOrEmpty(initialState))
            {
                bindingContext.ModelState.AddModelError(bindingContext.FieldName, "The initialState cannot be empty.");
                return Task.CompletedTask;
            }

            // You can add more validation here if needed (e.g., checking the format)
            bindingContext.Result = ModelBindingResult.Success(initialState);
            return Task.CompletedTask;
        }
    }
}
