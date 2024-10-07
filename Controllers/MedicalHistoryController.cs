using MEDManager.Data;
using MEDManager.Models;
using Microsoft.AspNetCore.Mvc;
namespace MEDManager.Controllers
{
    public class MedicalHistoryController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public MedicalHistoryController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
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

