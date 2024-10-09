using System.Linq;
using MEDManager.Data;
using MEDManager.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MEDManager.ViewModels;

namespace MEDManager.Controllers
{
    public class PatientController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<Doctor> _userManager;

        private string? _doctorId;

        public PatientController(ApplicationDbContext dbContext, UserManager<Doctor> userManager)
        {
            _userManager = userManager;
            _dbContext = dbContext;
        }
        // GET: MedicamentController
        public async Task<IActionResult> Index()
        {
            _doctorId = _userManager.GetUserId(User);
            return View(_dbContext.Patients.Where(p => p.DoctorId == _doctorId).ToList());
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var viewModel = new PatientViewModel
            {
                DoctorId = _userManager.GetUserId(User),
                MedicalHistories = await _dbContext.MedicalHistories.ToListAsync(),
                Allergies = await _dbContext.Allergies.ToListAsync(),
                SelectedMedicalHistoryIds = new List<int>(),
                SelectedAllergyIds = new List<int>()
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Patient patient, PatientViewModel viewModel)
        {
      

            if (!ModelState.IsValid)
            {
                return View();
            }

            patient.DoctorId = _doctorId;
            patient.Doctor = await _userManager.GetUserAsync(User);

            if (viewModel.SelectedAllergyIds != null)
            {
                var selectedAllergies = await _dbContext.Allergies
                    .Where(a => viewModel.SelectedAllergyIds.Contains(a.Id))
                    .ToListAsync();
                foreach (var allergy in selectedAllergies)
                {
                    patient.Allergies.Add(allergy);
                }
            }

            if (viewModel.SelectedMedicalHistoryIds != null)
            {
                var selectedMedicalHistories = await _dbContext.MedicalHistories
                    .Where(a => viewModel.SelectedMedicalHistoryIds.Contains(a.Id))
                    .ToListAsync();
                foreach (var medicalHistories in selectedMedicalHistories)
                {
                    patient.MedicalHistories.Add(medicalHistories);
                }
            }
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
        public async Task<IActionResult> Edit(int id)
        {
            var patient = await _dbContext.Patients
                .Include(p => p.MedicalHistories)
                .Include(p => p.Allergies)
                // .Include(p => p.Prescriptions)
                // .Include(p => p.Doctor)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (patient == null)
            {
                return NotFound();
            }

            var viewModel = new PatientViewModel
            {
                Patient = patient,
                DoctorId = _userManager.GetUserId(User),
                MedicalHistories = await _dbContext.MedicalHistories.ToListAsync(),
                Allergies = await _dbContext.Allergies.ToListAsync(),
                SelectedMedicalHistoryIds = patient.MedicalHistories.Select(m => m.Id).ToList() ?? new List<int>(),
                SelectedAllergyIds = patient.Allergies.Select(a => a.Id).ToList() ?? new List<int>(),
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, PatientViewModel viewModel)
        {
            if (id != viewModel.Patient.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var patient = await _dbContext.Patients
                        .Include(p => p.MedicalHistories)
                        .Include(p => p.Allergies)
                        .FirstOrDefaultAsync(p => p.Id == id);

                    if (patient == null)
                    {
                        return NotFound();
                    }

                    // Mise à jour des propriétés du patient
                    patient.FirstName = viewModel.Patient.FirstName;
                    patient.LastName = viewModel.Patient.LastName;
                    patient.Age = viewModel.Patient.Age;
                    patient.Gender = viewModel.Patient.Gender;
                    patient.Height = viewModel.Patient.Height;
                    patient.Weight = viewModel.Patient.Weight;
                    patient.DoctorId = patient.DoctorId;
                    patient.Doctor = patient.Doctor;

                    // Mise à jour des allergies
                    patient.Allergies.Clear();
                    if (viewModel.SelectedAllergyIds != null)
                    {
                        var selectedAllergies = await _dbContext.Allergies
                            .Where(a => viewModel.SelectedAllergyIds.Contains(a.Id))
                            .ToListAsync();
                        foreach (var allergy in selectedAllergies)
                        {
                            patient.Allergies.Add(allergy);
                        }
                    }

                    // Mise à jour des antécédents
                    patient.MedicalHistories.Clear();
                    if (viewModel.SelectedMedicalHistoryIds != null)
                    {
                        var selectedMedicalHistories = await _dbContext.MedicalHistories
                            .Where(a => viewModel.SelectedMedicalHistoryIds.Contains(a.Id))
                            .ToListAsync();
                        foreach (var medicalHistories in selectedMedicalHistories)
                        {
                            patient.MedicalHistories.Add(medicalHistories);
                        }
                    }

                    _dbContext.Entry(patient).State = EntityState.Modified;
                    await _dbContext.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PatientExists(viewModel.Patient.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            // Si nous arrivons ici, quelque chose a échoué, réafficher le formulaire
            viewModel.MedicalHistories = await _dbContext.MedicalHistories.ToListAsync();
            viewModel.Allergies = await _dbContext.Allergies.ToListAsync();
            return View(viewModel);
        }

        private bool PatientExists(int id)
        {
            return _dbContext.Patients.Any(p => p.Id == id);
        }
    }
}
