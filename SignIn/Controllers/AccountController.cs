using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using SignIn.Models.AccountViewModels;
using SignIn.Service;

namespace SignIn.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public AccountController(
            UserManager<ApplicationUser> userManager
          )
        {
            _userManager = userManager;
        }
        [HttpPost]
        [AllowAnonymous]
        public string Post(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = _userManager.CreateAsync(user, model.Password);

                return result.ToString();
            }
            else
            {
                return "";
            }
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(TestController.Index), "Home");
            }
        }
    }    
}