using Microsoft.AspNetCore.Identity;
using TechTreeWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using TechTreeWebApp.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using LoginModel = TechTreeWebApp.Models.LoginModel;
using RegistrationModel = TechTreeWebApp.Models.RegistrationModel;
using Microsoft.EntityFrameworkCore;
using TechTreeWebApp.Entities;

namespace TechTreeWebApp.Controllers
{
    public class UserAuthController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public UserAuthController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, ApplicationDbContext context) 
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _context = context;
        }
        [HttpPost]
        [AllowAnonymous] //no security restrictions are imposed
        [ValidateAntiForgeryToken] //denotes an action filter, request made to this filter are blocked unless the request a valid anti forgery token -- preventing cross site forgery
        public async Task<IActionResult> Login(LoginModel loginModel) 
        {
            loginModel.LoginInValid = "true";
            ApplicationUser signedUser = await _userManager.FindByEmailAsync(loginModel.Email); // UserName and Email get Interchanged
            if (ModelState.IsValid) 
            {
                var identityresult = await _signInManager.PasswordSignInAsync(signedUser.UserName, loginModel.Password, loginModel.RememberMe, lockoutOnFailure: true);
                                                                        //params: string userName, password, bool isPersistent
                if (identityresult.Succeeded)
                {
                    loginModel.LoginInValid = "";
                } 
                else
                {
                    ModelState.AddModelError(string.Empty, "Error, invalid login attempt");                    
                }
            }
            return PartialView("_UserLoginPartial", loginModel);
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout(string? returnUrl = null)
        {
            await _signInManager.SignOutAsync();
            if(returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index","Home");
            }
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterUser(RegistrationModel registerModel)
        {
            registerModel.RegistrationInValid = "true";
            // ModelState represents a collection of name and value pairs that was submitted to Post
            if (ModelState.IsValid) //is a property of controller object, can be accessed by UserAuth because it inherits it.
            {
                ApplicationUser applicationUser = new ApplicationUser()
                {
                    UserName = registerModel.UserName,
                    Email = registerModel.Email,
                    FirstName = registerModel.FirstName,
                    LastName = registerModel.LastName,
                    Address1 = registerModel.Address1,
                    Address2 = registerModel.Address2,
                    PostCode = registerModel.PostCode,
                    PhoneNumber = registerModel.PhoneNumber.ToString(),
                };
                //if(await UserNameExists(applicationUser.UserName))
                //{
                //    ModelState.AddModelError(string.Empty, "Error, Username already exists");
                //    return PartialView("_UserRegistrationPartial", registerModel);
                //}
                var identityresult = await _userManager.CreateAsync(applicationUser, registerModel.Password);                
                if (identityresult.Succeeded)
                {                    
                    registerModel.RegistrationInValid = "";
                    await _signInManager.SignInAsync(applicationUser, isPersistent: false);
                    if (registerModel.CategoryId != 0) // So public user choosed a course category
                    {
                        await AddCategoryToUser(applicationUser.Id, registerModel.CategoryId);
                    }
                    return PartialView("_UserRegistrationPartial", registerModel); //returning Client-side html code to _UserREgistrationPartial
                }
                AddErrortoModelState(identityresult);
            }            
            return PartialView("_UserRegistrationPartial", registerModel);
        }
        [AllowAnonymous]       
        public async Task<bool> UserNameExists(string userName)
        {
            var result = await _context.Users.AnyAsync(user => user.UserName.ToUpper() == userName.ToUpper());
            return (result) ? true : false;
        }
        private void AddErrortoModelState(IdentityResult identityResult) //	We should loop through the errors collec, made available on the Identity state, then send the relevant exception messages to the ModelState
        {
            foreach (var error in identityResult.Errors)
                ModelState.AddModelError(string.Empty, error.Description);
        }
        private async Task AddCategoryToUser(string userId, int categoryId) //cannot await void!
        {
            UserCategory userCategory = new UserCategory()
            {
                UserId = userId,
                CategoryId = categoryId
            };
            _context.UserCategory.Add(userCategory);
            await _context.SaveChangesAsync();
        }
    }
}
