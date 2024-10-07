using MEDManager.Data;
using MEDManager.Models;
using Microsoft.AspNetCore.Mvc;
namespace MEDManager.Controllers
{
    public class AllergyController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public AllergyController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
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
        public IActionResult Delete(int id)
        {
            Allergy? aller = _dbContext.Allergies.FirstOrDefault<Allergy>(a => a.Id == id);

            if (aller != null)
            {
                return View(aller);
            }

            return NotFound();
        }

        [HttpPost]

        public IActionResult DeleteConfirmed(int Id)
        {
            Allergy? aller = _dbContext.Allergies.FirstOrDefault<Allergy>(a => a.Id == Id);

            if (aller != null)
            {
                _dbContext.Allergies.Remove(aller);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return NotFound();
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
