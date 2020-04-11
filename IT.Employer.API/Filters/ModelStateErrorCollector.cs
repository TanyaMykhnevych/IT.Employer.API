using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Linq;

namespace IT.Employer.WebAPI.Filters
{
    public static class ModelStateErrorCollector
    {
        public static String GetErrors(ModelStateDictionary modelState)
        {
            if (modelState == null) throw new ArgumentNullException(nameof(modelState));

            return String.Join("; ", modelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage));
        }
    }
}
