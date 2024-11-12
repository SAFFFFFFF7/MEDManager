using MEDManager.Data;
using MEDManager.Models;
using MEDManager.ViewModel;
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

        private string? _doctorId => _userManager.GetUserId(User);

        public PrescriptionController(ApplicationDbContext dbContext, UserManager<Doctor> userManager)
        {
            _userManager = userManager;
            _dbContext = dbContext;
        }
        // GET: MedicamentController
        public async Task<IActionResult> Index()
        {
            try
            {
                return View(_dbContext.Prescriptions.Include(p => p.Patient).Where(p => p.DoctorId == _doctorId).ToList());
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
            if (_doctorId == null) return NotFound();

            try
            {
                var viewModel = new PatientListViewModel
                {
                    Patients = await _dbContext.Patients.Where(x => x.DoctorId == _doctorId).ToListAsync(),
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
        public async Task<IActionResult> Add(PatientListViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {
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

                return RedirectToAction("Edit", new { Id = prescription.Entity.Id });
            }
            catch (Exception ex)
            {
                View("Error");
            }
            return NotFound();
        }

        public IActionResult Delete(int id)
        {
            try
            {
                Prescription? pres = _dbContext.Prescriptions.FirstOrDefault<Prescription>(p => p.Id == id);

                if (pres != null)
                {
                    return View(pres);
                }
            }
            catch (Exception ex)
            {
                View("Error");
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int Id)
        {
            try
            {
                Prescription? pres = _dbContext.Prescriptions.FirstOrDefault<Prescription>(p => p.Id == Id);

                if (pres != null)
                {
                    _dbContext.Prescriptions.Remove(pres);
                    _dbContext.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                View("Error");
            }
            return NotFound();
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var prescription = await _dbContext.Prescriptions
                .Include(p => p.Patient)
                .ThenInclude(patient => patient.Allergies)
                .Include(p => p.Doctor)
                .Include(p => p.Medicaments)
                .Include(prescription => prescription.Patient)
                .ThenInclude(patient => patient.MedicalHistories)
                .FirstOrDefaultAsync(p => p.Id == id);

                if (prescription == null)
                {
                    return NotFound();
                }

                var allergiesPatient = prescription.Patient.Allergies.Select(pa => pa.Id).ToList();
                var medicalHistoryPatient = prescription.Patient.MedicalHistories.Select(mh => mh.Id).ToList();
                var medicamentList = await _dbContext.Medicaments
                    .Where(m => !m.Allergies.Any(a => allergiesPatient.Contains(a.Id)) && !m.MedicalHistories.Any(mh => medicalHistoryPatient.Contains(mh.Id)))
                    .ToListAsync();

                var model = new Tuple<Prescription, IEnumerable<Medicament>>(prescription, medicamentList);

                return View(model);
            }
            catch (Exception ex)
            {
                View("Error");
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, FormViewModel model)
        {
            if (id != model.PrescriptionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var prescription = await _dbContext.Prescriptions
                        .Include(p => p.Patient)
                        .Include(d => d.Doctor)
                        .Include(m => m.Medicaments)
                        .FirstOrDefaultAsync(p => p.Id == id);

                    if (prescription == null)
                    {
                        return NotFound();
                    }

                    prescription.StartDate = model.StartDate;
                    prescription.EndDate = model.EndDate;
                    prescription.Dosage = model.Dosage;
                    prescription.AdditionalInformation = model.AdditionalInformation;

                    prescription.Medicaments.Clear();

                    var selectedMedicaments = await _dbContext.Medicaments.Where(m => model.SelectedMedicamentIds.Contains(m.Id)).ToListAsync();

                    foreach (var medicament in selectedMedicaments) { prescription.Medicaments.Add(medicament); }

                    _dbContext.Entry(prescription).State = EntityState.Modified;
                    await _dbContext.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    if (!MedicamentExists(model.PrescriptionId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            model.Medicaments = await _dbContext.Medicaments.ToListAsync();
            return View(model);
        }

        private bool MedicamentExists(int id)
        {
            return _dbContext.Medicaments.Any(m => m.Id == id);
        }
    }
}
