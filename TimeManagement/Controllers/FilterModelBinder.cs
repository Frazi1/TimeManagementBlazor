using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Domain
{
    public class FilterModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            ValueProviderResult skip = bindingContext.ValueProvider.GetValue("skip");
            ValueProviderResult take = bindingContext.ValueProvider.GetValue("take");

            if (int.TryParse(skip.FirstValue, out int skipValue) == false)
                return Task.FromResult(false);

            if (int.TryParse(take.FirstValue, out int takeValue) == false)
                return Task.FromResult(false);

            bindingContext.Result = ModelBindingResult.Success(new Filter(skipValue, takeValue));
            return Task.FromResult(true);
        }
    }
}