using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace _01.Net_Core_Restful_API.Services
{
    public class PropertyCheckService: IPropertyCheckerService
    {
        public bool TypeHasProperties<T>(string fields)
        {
            if (string.IsNullOrWhiteSpace(fields))
            {
                return true;
            }

            var fieldsAfterSplit = fields.Split(",");
            foreach (var field in fieldsAfterSplit)
            {
                var propertyName = fields.Trim();
                var propertyInfo = typeof(T).GetProperty(propertyName, BindingFlags.Public | BindingFlags.IgnoreCase
                    | BindingFlags.Instance);
                if (propertyInfo == null)
                {
                    return false;
                }
            }
            return true;

        }
    }
}
