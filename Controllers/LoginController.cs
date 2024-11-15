using Domain.ViewModels;
using GosAutoAPI.IServices.Auth;
using GosAutoAPI.Services.Auth;
using Microsoft.AspNetCore.Mvc;

namespace GosAutoAPI.Controllers
{
    public class LoginController : Controller
    {
        private readonly IAuthentication _authentication;

        public LoginController(IAuthentication authentication)
        {
            _authentication = authentication;

        }

        [HttpGet]
        public IActionResult Index(string username)
        {
            return View(new AuthViewModel()
            {
                UserName = username
            });
        }

        [HttpPost]
        public async Task<IActionResult> IndexPost(AuthViewModel model)
        {
            if (ModelState.IsValid)
            {

                var isAuthenticated = await _authentication.Authenticate(model.UserName!, model.Password!,
                        Request.HttpContext.Connection.RemoteIpAddress?.ToString() ?? "", model.RememberMe);
                if (isAuthenticated)
                    return Redirect(String.IsNullOrEmpty(model.UserName) ? "/" : model.UserName);
                else
                    ModelState.TryAddModelError("UserName", "Неверный пользователь или пароль");
            }
            return View("Index", model);
        }
    }
}
