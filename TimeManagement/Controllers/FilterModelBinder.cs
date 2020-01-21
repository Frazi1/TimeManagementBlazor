using System;
using System.Text.RegularExpressions;
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
            ValueProviderResult orderBy = bindingContext.ValueProvider.GetValue("order_by");

            if (int.TryParse(skip.FirstValue, out int skipValue) == false)
                return Task.FromResult(false);

            if (int.TryParse(take.FirstValue, out int takeValue) == false)
                return Task.FromResult(false);

            var sorting = orderBy.FirstValue.Split(",");
            PropertySorting propertySorting = null;
            if (sorting.Length == 2)
            {
                string propertyName = sorting[0];
                bool isAsc;
                if (sorting[1] == "asc") isAsc = true;
                else if(sorting[1] == "desc") isAsc = false;
                else return Task.FromResult(false);
                propertySorting = new PropertySorting(propertyName, isAsc);
            }

            bindingContext.Result = ModelBindingResult.Success(new Filter(
                skipValue,
                takeValue,
                propertySorting != null ? new[] {propertySorting} : null)
            );
            return Task.FromResult(true);
        }
    }
}