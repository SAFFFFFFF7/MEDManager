using MEDManager.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MEDManager.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<Doctor> _signInManager; // permet de gerer la connexion et la deconnexion des utilisateurs, nous est fourni par ASP.NET Core Identity

        public AccountController(SignInManager<Doctor> signInManager)
        {
            _signInManager = signInManager; // Signin manager est injecté dans le constructeur,
                                            // c'est une classe generique qui prend en parametre ApplicationUser
        }

        public IActionResult Login()
        {
            return View(); // Affiche la vue Login
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }

            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}
