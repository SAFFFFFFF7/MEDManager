using MEDManager.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MEDManager.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<Doctor> _signInManager; // permet de gerer la connexion et la deconnexion des utilisateurs, nous est fourni par ASP.NET Core Identity

        private readonly UserManager<Doctor> _userManager;

        public AccountController(SignInManager<Doctor> signInManager, UserManager<Doctor> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
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
                var result = await _signInManager.PasswordSignInAsync(model.UserName , model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }

            return View(model);
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View(); // permet d'afficher dans le navigateur la vue qui se trouve dans : /Views/Account/Register.cshtml
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {

                var doctor = new Doctor {
                    UserName = model.UserName,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                };

                var result = await _userManager.CreateAsync(doctor, model.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(doctor, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}
