using Microsoft.AspNetCore.Mvc.Rendering;
using TechTreeWebApp.Interfaces;

namespace TechTreeWebApp.Extentions
{
    public static class ConvertExtentions
    {
        internal static List<SelectListItem> ConvertToSelectList<T>(this IEnumerable<T> collection, int selectedValue)where T: IPrimaryProperties //a constraint for the compiler
        {
            return (from item in collection
                    select new SelectListItem
                    {
                        Text = item.Title,
                        Value = item.Id.ToString(),
                        Selected = (item.Id == selectedValue)
                    }).ToList();
        }
    }
}
