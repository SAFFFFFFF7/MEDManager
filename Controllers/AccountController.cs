using MEDManager.Data;
using MEDManager.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace MEDManager.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<Doctor> _signInManager; // permet de gerer la connexion et la deconnexion des utilisateurs, nous est fourni par ASP.NET Core Identity

        private readonly UserManager<Doctor> _userManager;
        private readonly ApplicationDbContext _dbContext;

        private Doctor doctor1;


        public AccountController(ApplicationDbContext dbContext, SignInManager<Doctor> signInManager, UserManager<Doctor> userManager)
        {
            _dbContext = dbContext;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // Récupérer le nom d'utilisateur connecté
            var userName = User.Identity.Name;

            // Requête pour obtenir les informations du docteur
            var doctor = await _dbContext.Users
                .Where(u => u.UserName == userName) // Assurez-vous que UserName correspond à la colonne dans la table
                .Select(d => new RegisterViewModel
                {
                    FirstName = d.FirstName,
                    LastName = d.LastName,
                    Email = d.Email
                    // Mappez d'autres propriétés si nécessaire
                })
                .FirstOrDefaultAsync();

            if (doctor == null)
            {
                return NotFound("Informations non trouvées.");
            }

            return View(doctor);
        }

        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            var user = _userManager.GetUserId(User);

            var doctor = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == user);

            var model = new RegisterViewModel
            {
                UserName = doctor.UserName,
                Password = doctor.PasswordHash,
                FirstName = doctor.FirstName,
                LastName = doctor.LastName,
                Email = doctor.Email
            };

            if (doctor == null)
            {
                return NotFound("Utilisateur non trouvé.");
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model); // Retourne la vue avec les erreurs de validation
            }

            var user = _userManager.GetUserId(User);

            var doctor = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == user);

            if (doctor == null)
            {
                return NotFound("Utilisateur non trouvé.");
            }

            // Mise à jour des informations
            var passwordHasher = new PasswordHasher<Doctor>();
            doctor.PasswordHash = passwordHasher.HashPassword(doctor, model.Password);
            doctor.UserName = model.UserName;
            doctor.NormalizedUserName = model.UserName;
            doctor.FirstName = model.FirstName;
            doctor.LastName = model.LastName;
            doctor.Email = model.Email;
            doctor.NormalizedEmail = model.Email;

            // Enregistrer les modifications
            _dbContext.Update(doctor);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Edit"); // Redirige vers la vue des informations utilisateur
        }


        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Dashboard");
            }
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
                    return RedirectToAction("Index", "Dashboard");
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

                var doctor = new Doctor
                {
                    UserName = model.UserName,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                };

                var result = await _userManager.CreateAsync(doctor, model.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(doctor, isPersistent: false);
                    return RedirectToAction("Index", "Dashboard");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

            }

            return RedirectToAction("Register", "Account");
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Login", "Account");
        }
    }
}
