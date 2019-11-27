using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proj1.Extensions
{
    public static class RefClass
    {
        public static string GetPropertyValue<T>(this T item, string propertyName)
        {
            return item.GetType().GetProperty(propertyName).GetValue(item,null).ToString();
        }
    }
}
