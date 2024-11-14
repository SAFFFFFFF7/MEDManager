using MEDManager.Data;
using MEDManager.Models;
using MEDManager.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MedManager.Controllers;

[Authorize]
public class PrescriptionController : Controller
{
    private readonly ApplicationDbContext _dbContext;
    private readonly UserManager<Doctor> _userManager;

    private string? _doctorId => _userManager.GetUserId(User);

    public PrescriptionController(ApplicationDbContext dbContext, UserManager<Doctor> userManager)
    {
        _dbContext = dbContext;
        _userManager = userManager;
    }

    [HttpGet]
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
        PatientListViewModel model = new();

        var doctorId = _userManager.GetUserId(User);
        if (doctorId == null) return NotFound();

        model.Patients = await _dbContext.Patients.AsNoTracking().Where(p => p.DoctorId == doctorId).ToListAsync();

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Add(PatientListViewModel model)
    {
        var doctorId = _userManager.GetUserId(User);
        if (doctorId == null) return NotFound();

        if (!ModelState.IsValid)
        {
            model.Patients = _dbContext.Patients.Where(p => p.DoctorId == doctorId).ToList();
            return View(model);
        }

        var patient = await _dbContext.Patients.FirstOrDefaultAsync(p => p.Id == model.PatientId);
        if (patient == null) return NotFound();

        var prescription = await _dbContext.Prescriptions.AddAsync(new Prescription
        {
            PatientId = patient.Id,
            Patient = patient,
            DoctorId = doctorId,
            Doctor = (await _dbContext.Users.FirstOrDefaultAsync(d => d.Id == doctorId))!,
        });

        await _dbContext.SaveChangesAsync();

        return RedirectToAction("Edit", new { id = prescription.Entity.Id });
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
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

        var model = new PrescriptionViewModel()
        {
            AdditionalInformation = prescription.AdditionalInformation,
            Dosage = prescription.Dosage,
            EndDate = prescription.EndDate,
            StartDate = prescription.StartDate,
            MedicamentsPatient = medicamentList,
            PrescriptionId = prescription.Id,
            Patient = prescription.Patient,
            MedicamentsPrescription = prescription.Medicaments
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(PrescriptionViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var p = await _dbContext.Prescriptions
                .Include(p => p.Patient)
                .ThenInclude(patient => patient.Allergies)
                .Include(p => p.Doctor)
                .Include(p => p.Medicaments)
                .Include(prescription => prescription.Patient)
                .ThenInclude(patient => patient.MedicalHistories)
                .FirstOrDefaultAsync(p => p.Id == model.PrescriptionId);

            var allergiesPatient = p.Patient.Allergies.Select(pa => pa.Id).ToList();
            var medicalHistoryPatient = p.Patient.MedicalHistories.Select(mh => mh.Id).ToList();
            var medicamentList = await _dbContext.Medicaments
                .Where(m => !m.Allergies.Any(a => allergiesPatient.Contains(a.Id)) && !m.MedicalHistories.Any(mh => medicalHistoryPatient.Contains(mh.Id)))
                .ToListAsync();

            model.Patient = p.Patient;
            model.MedicamentsPatient = medicamentList;
            model.MedicamentsPrescription = p.Medicaments;

            return View(model);
        }

        var prescription = _dbContext.Prescriptions.FirstOrDefault(p => p.Id == model.PrescriptionId);
        if (prescription == null)
        {
            return NotFound();
        }

        if (model.EndDate < model.StartDate)
        {
            ModelState.AddModelError("EndDate", "La date de fin doit être supérieure à la date de début.");
            return RedirectToAction("Edit", new { id = model.PrescriptionId });
        }

        prescription.StartDate = model.StartDate;
        prescription.EndDate = model.EndDate;
        prescription.Dosage = model.Dosage;
        prescription.AdditionalInformation = model.AdditionalInformation;

        _dbContext.Prescriptions.Update(prescription);
        _dbContext.SaveChanges();

        return RedirectToAction("Edit", new { id = prescription.Id });
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            Prescription? prescriptionToDelete = await _dbContext.Prescriptions.Where(p => p.Id == id).FirstOrDefaultAsync();
            if (prescriptionToDelete == null) return NotFound();
            
            _dbContext.Prescriptions.Remove(prescriptionToDelete);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index", "Prescription");
        }
        catch (DbUpdateException ex)
        {
            return RedirectToAction("Index", "Prescription");
        }
        catch (Exception ex)
        {
            return RedirectToAction("Index", "Prescription");
        }
    }

    [HttpGet]
    public IActionResult AddMedicament(int id, int medicamentId)
    {
        var medicament = _dbContext.Medicaments.FirstOrDefault(m => m.Id == medicamentId);

        if (medicament == null)
        {
            return RedirectToAction("Edit", new { id });
        }

        try
        {
            var prescription = _dbContext.Prescriptions
                .Include(p => p.Medicaments)
                .FirstOrDefault(p => p.Id == id);

            if (prescription == null)
            {
                return RedirectToAction("Edit", new { id });
            }

            prescription.Medicaments.Add(medicament);
            _dbContext.SaveChanges();
        }
        catch
        {
            // ignored
        }

        return RedirectToAction("Edit", new { id });
    }

    public IActionResult RemoveMedicament(int id, int medicamentId)
    {
        var prescription = _dbContext.Prescriptions
            .Include(p => p.Medicaments)
            .FirstOrDefault(p => p.Id == id);

        var medicament = prescription?.Medicaments.FirstOrDefault(m => m.Id == medicamentId);

        if (prescription == null || medicament == null) return RedirectToAction("Edit", new { id });

        try
        {
            prescription.Medicaments.Remove(medicament);
            _dbContext.SaveChanges();
        }
        catch
        {
            // ignored
        }

        return RedirectToAction("Edit", new { id });
    }
}