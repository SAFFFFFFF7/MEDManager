using MEDManager.Data;
using MEDManager.Models;
using Microsoft.AspNetCore.Mvc;

namespace MEDManager.Controllers
{
    public class MedicamentController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public MedicamentController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        // GET: MedicamentController
        public ActionResult Index()
        {
            return View(_dbContext.Medicaments);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Medicament medicament)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            _dbContext.Medicaments.Add(medicament);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            Medicament? medi = _dbContext.Medicaments.FirstOrDefault<Medicament>(s => s.Id == id);

            if (medi != null)
            {
                return View(medi);
            }

            return NotFound();
        }

        [HttpPost]

        public IActionResult DeleteConfirmed(int Id)
        {
            Medicament? medi = _dbContext.Medicaments.FirstOrDefault<Medicament>(m => m.Id == Id);

            if (medi != null)
            {
                _dbContext.Medicaments.Remove(medi);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return NotFound();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Medicament? medi = _dbContext.Medicaments.FirstOrDefault<Medicament>(m => m.Id == id);

            if (medi != null)
            {
                return View(medi);
            }

            return NotFound();
        }

        [HttpPost]
        public IActionResult Edit(Medicament medicament)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            Medicament? medi = _dbContext.Medicaments.FirstOrDefault<Medicament>(m => m.Id == medicament.Id);

            if (medi != null)
            {
                medi.Name = medicament.Name;
                medi.Quantity = medicament.Quantity;
                medi.Ingredients = medicament.Ingredients;

                _dbContext.SaveChanges();

                return RedirectToAction("Index");
            }

            return NotFound();
        }
    }
}