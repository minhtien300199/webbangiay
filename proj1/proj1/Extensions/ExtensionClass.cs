using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace proj1.Extensions
{
    public static class ExtensionClass
    {
        public static IEnumerable<SelectListItem> ToSelectListItems<T>(this IEnumerable<T> items, int selectedValue)
        {
            return from item in items
                   select new SelectListItem
                   {
                       Text = item.GetPropertyValue("BrandName"),
                       Value = item.GetPropertyValue("BrandID"),
                       Selected = item.GetPropertyValue("BrandID").Equals(selectedValue.ToString())
                   };
        }
        public static IEnumerable<SelectListItem> TypeSelect<T>(this IEnumerable<T> items, int selectedValue)
        {
            return from item in items
                   select new SelectListItem
                   {
                       Text = item.GetPropertyValue("NameType"),
                       Value = item.GetPropertyValue("TypeId"),
                       Selected = item.GetPropertyValue("TypeId").Equals(selectedValue.ToString())
                   };
        }
        public static IEnumerable<SelectListItem> ProviderSelect<T>(this IEnumerable<T> items, int selectedValue)
        {
            return from item in items
                   select new SelectListItem
                   {
                       Text = item.GetPropertyValue("ProviderName"),
                       Value = item.GetPropertyValue("ProviderID"),
                       Selected = item.GetPropertyValue("ProviderID").Equals(selectedValue.ToString())
                   };
        }
    }
}
