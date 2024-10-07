using MEDManager.Data;
using MEDManager.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MEDManager.Controllers
{
    public class PatientController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<Doctor> _userManager;

        public PatientController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        // GET: MedicamentController
        public ActionResult Index()
        {
            return View(_dbContext.Patients);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Patient patient)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // patient.DoctorId = _userManager();

            _dbContext.Patients.Add(patient);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            Patient? pati = _dbContext.Patients.FirstOrDefault<Patient>(p => p.Id == id);

            if (pati != null)
            {
                return View(pati);
            }

            return NotFound();
        }

        [HttpPost]

        public IActionResult DeleteConfirmed(int Id)
        {
            Patient? pati = _dbContext.Patients.FirstOrDefault<Patient>(p => p.Id == Id);

            if (pati != null)
            {
                _dbContext.Patients.Remove(pati);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return NotFound();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Patient? pati = _dbContext.Patients.FirstOrDefault<Patient>(p => p.Id == id);

            if (pati != null)
            {
                return View(pati);
            }

            return NotFound();
        }

        [HttpPost]
        public IActionResult Edit(Patient patient)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            Patient? pati = _dbContext.Patients.FirstOrDefault<Patient>(p => p.Id == patient.Id);

            if (pati != null)
            {
                pati.FirstName = patient.FirstName;
                pati.LastName = patient.LastName;
                pati.Age = patient.Age;

                _dbContext.SaveChanges();

                return RedirectToAction("Index");
            }

            return NotFound();
        }
    }
}
