using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TechTreeWebApp.Data;

namespace TechTreeWebApp.Filters
{
    public class AdminAreaAuthorizationFilter : IAsyncAuthorizationFilter
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminAreaAuthorizationFilter(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var user = await _userManager.GetUserAsync(context.HttpContext.User);
            if (user != null && await _userManager.IsInRoleAsync(user, "admin"))
            {
                var routeValues = new { area = "Admin", controller = "Home", action = "Index" };
                context.Result = new RedirectToActionResult("Index", "Home", routeValues);
            }
        }
    }
}
