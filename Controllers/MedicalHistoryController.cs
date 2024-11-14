using MEDManager.Data;
using MEDManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
namespace MEDManager.Controllers
{
    [Authorize]
    public class MedicalHistoryController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public MedicalHistoryController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Find(string searchString)
        {
            if (_dbContext.MedicalHistories == null)
            {
                return Problem("Entity set 'MvcMovieContext.Movie'  is null.");
            }

            List<MedicalHistory> medicalHistories = new();

            if (!String.IsNullOrEmpty(searchString))
            {
                medicalHistories = _dbContext.MedicalHistories.Where(s => s.Name!.ToUpper().Contains(searchString.ToUpper())).ToList();
            }

            return View("Index", medicalHistories);
        }

        [HttpPost]
        public string Find(string searchString, bool notUsed)
        {
            return "From [HttpPost]Index: filter on " + searchString;
        }
        
        // GET: AllergyController
        public ActionResult Index()
        {
            return View(_dbContext.MedicalHistories);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(MedicalHistory medicalHistory)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            _dbContext.MedicalHistories.Add(medicalHistory);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            MedicalHistory? mediHist = _dbContext.MedicalHistories.FirstOrDefault<MedicalHistory>(mh => mh.Id == id);

            if (mediHist != null)
            {
                return View(mediHist);
            }

            return NotFound();
        }

        [HttpPost]

        public IActionResult DeleteConfirmed(int Id)
        {
            MedicalHistory? mediHist = _dbContext.MedicalHistories.FirstOrDefault<MedicalHistory>(mh => mh.Id == Id);

            if (mediHist != null)
            {
                _dbContext.MedicalHistories.Remove(mediHist);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return NotFound();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            MedicalHistory? mediHist = _dbContext.MedicalHistories.FirstOrDefault<MedicalHistory>(mh => mh.Id == id);

            if (mediHist != null)
            {
                return View(mediHist);
            }

            return NotFound();
        }

        [HttpPost]
        public IActionResult Edit(MedicalHistory medicalHistory)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            MedicalHistory? mediHist = _dbContext.MedicalHistories.FirstOrDefault<MedicalHistory>(mh => mh.Id == medicalHistory.Id);

            if (mediHist != null)
            {
                mediHist.Name = medicalHistory.Name;

                _dbContext.SaveChanges();

                return RedirectToAction("Index");
            }

            return NotFound();
        }

    }
}

