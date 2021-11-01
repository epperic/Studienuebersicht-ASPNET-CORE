using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Studienuebersicht.Model;
using Studienuebersicht.MVC.ViewModel;

namespace Studienuebersicht.MVC.Controllers
{
    public class AuthenticationController : Controller
    {
        private IRepository repository;

        public AuthenticationController(IRepository repository)
        {
            this.repository = repository;
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult CheckCredentials([FromForm] Login login)
        {
            var account = repository.Accounts.FindAdminByEmailAndPassword(login.Email, login.Password);

            if (account == null)
            {
                ModelState.AddModelError("", "A user with these credentials could not be found!");
                return View("Login", login);
            }

            HttpContext.Session.SetInt32("account_id", account.Id);
            return Redirect("/Home");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.Items["account"] = null;
            return Redirect("/Authentication/Login");
        }
    }
}
