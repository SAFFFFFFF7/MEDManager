using System.Linq;
using MEDManager.Data;
using MEDManager.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MEDManager.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace MEDManager.Controllers
{
    [Authorize]
    public class PatientController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<Doctor> _userManager;

        private string? _doctorId => _userManager.GetUserId(User);

        public PatientController(ApplicationDbContext dbContext, UserManager<Doctor> userManager)
        {
            _userManager = userManager;
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Find(string searchString)
        {
            try
            {
                if (_dbContext.Patients == null)
                {
                    return Problem("Entity set 'MvcMovieContext.Movie'  is null.");
                }

                List<Patient> patients = new();

                if (!String.IsNullOrEmpty(searchString))
                {
                    patients = _dbContext.Patients.Where(s => s.FirstName!.ToUpper().Contains(searchString.ToUpper()) || s.LastName!.ToUpper().Contains(searchString.ToUpper())).ToList();
                    return View("Index", patients);
                }
                else
                {
                    patients = _dbContext.Patients.ToList();    
                    return View("Index", patients);
                }
            }
            catch (Exception ex)
            {
                View("Error");
            }
            return NotFound();
        }

        [HttpPost]
        public string Find(string searchString, bool notUsed)
        {
            return "From [HttpPost]Index: filter on " + searchString;
        }

        // GET: MedicamentController
        public async Task<IActionResult> Index()
        {
            try
            {
                return View(_dbContext.Patients.Where(p => p.DoctorId == _doctorId).ToList());
            }
            catch (Exception ex)
            {
                View("Error");
            }
            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> ShowDetails(int id)
        {
            try
            {
                var patient = await _dbContext.Patients
                    .Include(p => p.MedicalHistories)
                    .Include(p => p.Allergies)
                    .Include(p => p.Prescriptions)
                    .FirstOrDefaultAsync(p => p.Id == id);

                if (patient != null)
                {
                    return View(patient);
                }
            }
            catch (Exception ex)
            {
                View("Error");
            }
            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            try
            {
                var viewModel = new PatientViewModel
                {
                    MedicalHistories = await _dbContext.MedicalHistories.ToListAsync(),
                    Allergies = await _dbContext.Allergies.ToListAsync(),
                    SelectedMedicalHistoryIds = new List<int>(),
                    SelectedAllergyIds = new List<int>(),
                    DrpMedicalHistories = _dbContext.MedicalHistories.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList(),
                    DrpAllergies = _dbContext.Allergies.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList()
                };
                return View(viewModel);
            }
            catch (Exception ex)
            {
                View("Error");
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Add(PatientViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    viewModel.DrpMedicalHistories = _dbContext.MedicalHistories.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
                    viewModel.DrpAllergies = _dbContext.Allergies.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
                    return View(viewModel);
                }

                Patient patient = new Patient
                {
                    FirstName = viewModel.FirstName,
                    LastName = viewModel.LastName,
                    Age = viewModel.Age,
                    Gender = viewModel.Gender,
                    Height = viewModel.Height,
                    Weight = viewModel.Weight,
                    SecurityCardNumber = viewModel.SecurityCardNumber,
                    DoctorId = _doctorId
                };

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
            catch (Exception ex)
            {
                View("Error");
            }
            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                Patient? patientToDelete = await _dbContext.Patients.Where(p => p.Id == id).FirstOrDefaultAsync();
                if (patientToDelete == null) return NotFound();

                _dbContext.Patients.Remove(patientToDelete);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction("Index", "Patient");
            }
            catch (DbUpdateException ex)
            {
                return RedirectToAction("Index", "Patient");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Patient");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
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

                var viewModel = new PatientViewModel
                {
                    PatientId = id,
                    FirstName = patient.FirstName,
                    LastName = patient.LastName,
                    Age = patient.Age,
                    Gender = patient.Gender,
                    Height = patient.Height,
                    Weight = patient.Weight,
                    SecurityCardNumber = patient.SecurityCardNumber,
                    MedicalHistories = await _dbContext.MedicalHistories.ToListAsync(),
                    Allergies = await _dbContext.Allergies.ToListAsync(),
                    SelectedMedicalHistoryIds = patient.MedicalHistories.Select(m => m.Id).ToList() ?? new List<int>(),
                    SelectedAllergyIds = patient.Allergies.Select(a => a.Id).ToList() ?? new List<int>(),
                    DrpMedicalHistories = _dbContext.MedicalHistories.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList(),
                    DrpAllergies = _dbContext.Allergies.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList()
                };
                return View(viewModel);
            }
            catch (DbUpdateException ex)
            {
                View("Error");
            }
            return NotFound();

        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, PatientViewModel viewModel)
        {
            if (id != viewModel.PatientId)
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
                    patient.FirstName = viewModel.FirstName;
                    patient.LastName = viewModel.LastName;
                    patient.Age = viewModel.Age;
                    patient.Gender = viewModel.Gender;
                    patient.Height = viewModel.Height;
                    patient.Weight = viewModel.Weight;
                    patient.SecurityCardNumber = viewModel.SecurityCardNumber;
                    patient.DoctorId = patient.DoctorId;
                    patient.Doctor = patient.Doctor;


                    // Mise à jour des allergies
                    patient.Allergies.Clear();
                    if (viewModel.SelectedAllergyIds != null && viewModel.SelectedAllergyIds.Count > 0)
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
                    if (viewModel.SelectedMedicalHistoryIds != null && viewModel.SelectedMedicalHistoryIds.Count > 0)
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
                    if (!PatientExists(viewModel.PatientId))
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
            viewModel.DrpMedicalHistories = _dbContext.MedicalHistories.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
            viewModel.DrpAllergies = _dbContext.Allergies.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
            return View(viewModel);
        }

        private bool PatientExists(int id)
        {
            return _dbContext.Patients.Any(p => p.Id == id);
        }
    }
}
