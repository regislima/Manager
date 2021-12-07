using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Manager.Core.Extensions
{
    public static class Extensions
    {
        public static bool IsNull(this object obj)
        {
            if (obj is null)
                return true;

            return false;
        }

        public static string GetErrorMessage(this ModelStateDictionary dictionary)
        {
            return dictionary.SelectMany(m => m.Value.Errors)
                    .Select(m => m.ErrorMessage)
                    .First();  
        }
    }
}