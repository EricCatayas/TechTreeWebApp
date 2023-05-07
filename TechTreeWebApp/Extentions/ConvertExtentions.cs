using Microsoft.AspNetCore.Mvc.Rendering;
using TechTreeWebApp.Interfaces;
using TechTreeWebApp.Models;

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
        public static IEnumerable<GroupedCategoryItemsByCategoryModel> GetGroupedCategoryItemsByCategoryModels(this IEnumerable<CategoryItemDetailsModel> categoryItemDetailsModels)
        {
            return (from item in categoryItemDetailsModels
                    group item by item.CategoryId into g
                    select new GroupedCategoryItemsByCategoryModel
                    {
                        Id = g.Key,
                        Title = g.Select(c => c.CategoryTitle).FirstOrDefault(),
                        Items = g
                    });
        }
    }
}
