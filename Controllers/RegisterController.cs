using Domain.ViewModels;
using GosAutoAPI.IServices.Auth;
using Microsoft.AspNetCore.Mvc;

namespace GosAutoAPI.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IAuthentication _authentication;


        public RegisterController(IAuthentication authentication)
        {
            _authentication = authentication;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new RegisterViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> IndexPost(RegisterViewModel model)
        {


                var userError = await _authentication.
                if (emailError != null)
                    ModelState.TryAddModelError("Email", emailError.ErrorMessage!);

                if (ModelState.IsValid)
                {
                    await authentication.CreateUser(model.ToUserModel());
                    return Redirect("/");
                }
            }
            else
            {
                ModelState.TryAddModelError("captcha", "Incorrect Captcha");
            }
            return View("Index", model);
        }
    }
}
