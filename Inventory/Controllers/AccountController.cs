using Inventory.Application.ViewModels;
using Inventory.DAL.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Inventory.Web.Controllers
{
    public class AccountController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor;



        public AccountController( UserManager<ApplicationUser> userManager,
                              SignInManager<ApplicationUser> signInManager,
                              IHttpContextAccessor httpContextAccessor)
        {

            _userManager = userManager;
            _signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;
        }


        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View("Index");
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(AuthenticateRequestViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await _userManager.FindByEmailAsync(model.Username);
                if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
                {
                    ClaimsIdentity identity = new ClaimsIdentity(IdentityConstants.ApplicationScheme);
                    identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
                    identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
                    identity.AddClaim(new Claim(ClaimTypes.Email, user.Email));
                    identity.AddClaim(new Claim(ClaimTypes.GivenName, user.FirstName + " " + user.LastName));

                    IList<string> roles = await _userManager.GetRolesAsync(user);

                    roles.ToList().ForEach(e =>
                    {
                        identity.AddClaim(new Claim(ClaimTypes.Role, e));
                    });

                    await HttpContext.SignInAsync(IdentityConstants.ApplicationScheme, new ClaimsPrincipal(identity));
                    return RedirectToAction("Index","Home");
                }
                return View("Index");
            }
            else
            {
                ModelState.AddModelError("", "Invalid UserName or Password");
                return View();
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Redirect($"{nameof(AccountController.Login)}");
        }

        [HttpGet]
        public  IActionResult SignUp()
        {
            return View();
        }

        public async Task<IActionResult> SignUp(ApplicationUserViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ApplicationUser user = new ApplicationUser
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Email = model.Email,
                        UserName = model.UserName,
                    };
                    await _userManager.CreateAsync(user, model.Password);
                    return Redirect(nameof(Login));
                }
                else
                    return View(model);
            }
            catch (Exception ex)
            {
                return View(model);
            }
        }
       
    }
}
