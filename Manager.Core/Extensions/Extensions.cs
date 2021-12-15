using System.ComponentModel;
using System.Linq;
using System.Reflection;
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

        public static string GetEnumDescription<TEnum>(this TEnum @enum)
        {
            FieldInfo info = @enum.GetType().GetField(@enum.ToString());
            var attributes = (DescriptionAttribute[])info.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return attributes?[0].Description ?? @enum.ToString();
        }
    }
}