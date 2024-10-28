using MEDManager.Data;
using MEDManager.Models;
using MEDManager.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MEDManager.Controllers
{
    public class PrescriptionController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<Doctor> _userManager;

        private string? _doctorId;

        public PrescriptionController(ApplicationDbContext dbContext, UserManager<Doctor> userManager)
        {
            _userManager = userManager;
            _dbContext = dbContext;
        }
        // GET: MedicamentController
        public async Task<IActionResult> Index()
        {
            _doctorId = _userManager.GetUserId(User);
            return View(_dbContext.Prescriptions.Include(p => p.Patient).Where(p => p.DoctorId == _doctorId).ToList());
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var doctorId = _userManager.GetUserId(User);
            if (doctorId == null) return NotFound();

            var viewModel = new PatientListViewModel
            {
                Patients = await _dbContext.Patients.Where(x => x.DoctorId == doctorId).ToListAsync(),
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Add(PatientListViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var patient = await _dbContext.Patients.FirstOrDefaultAsync(p => p.Id == viewModel.PatientId);
            if (patient == null) return NotFound();

            var doctorId = _userManager.GetUserId(User);
            if (doctorId == null) return NotFound();

            var doctor = await _dbContext.Users.FirstOrDefaultAsync(d => d.Id == doctorId);
            if (doctor == null) return NotFound();

            var prescription = await _dbContext.Prescriptions.AddAsync(new Prescription
            {
                PatientId = patient.Id,
                Patient = patient,
                DoctorId = doctorId,
                Doctor = doctor,

            });

            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Edit", new {Id = prescription.Entity.Id});
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var prescription = await _dbContext.Prescriptions
                .Include(p => p.Patient)
                .Include(p => p.Doctor)
                .Include(p => p.Medicaments)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (prescription == null)
            {
                return NotFound();
            }

            return View(prescription);
        }
    }
}
