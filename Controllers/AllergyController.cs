using MEDManager.Data;
using MEDManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
namespace MEDManager.Controllers
{
    [Authorize]
    public class AllergyController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public AllergyController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Find(string searchString)
        {
            if (_dbContext.Allergies == null)
            {
                return Problem("Entity set 'MvcMovieContext.Movie'  is null.");
            }

            List<Allergy> allergies = new();

            if (!String.IsNullOrEmpty(searchString))
            {
                allergies = _dbContext.Allergies.Where(s => s.Name!.ToUpper().Contains(searchString.ToUpper())).ToList();
            }

            return View("Index", allergies);
        }

        [HttpPost]
        public string Find(string searchString, bool notUsed)
        {
            return "From [HttpPost]Index: filter on " + searchString;
        }

        // GET: AllergyController
        public ActionResult Index()
        {
            return View(_dbContext.Allergies);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Allergy allergy)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            _dbContext.Allergies.Add(allergy);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                Allergy? allergyToDelete = await _dbContext.Allergies.Where(p => p.Id == id).FirstOrDefaultAsync();
                if (allergyToDelete == null) return NotFound();

                _dbContext.Allergies.Remove(allergyToDelete);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction("Index", "Allergy");
            }
            catch (DbUpdateException ex)
            {
                return RedirectToAction("Index", "Allergy");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Allergy");
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Allergy? aller = _dbContext.Allergies.FirstOrDefault<Allergy>(a => a.Id == id);

            if (aller != null)
            {
                return View(aller);
            }

            return NotFound();
        }

        [HttpPost]
        public IActionResult Edit(Allergy allergy)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            Allergy? aller = _dbContext.Allergies.FirstOrDefault<Allergy>(a => a.Id == allergy.Id);

            if (aller != null)
            {
                aller.Name = allergy.Name;

                _dbContext.SaveChanges();

                return RedirectToAction("Index");
            }

            return NotFound();
        }

    }
}
