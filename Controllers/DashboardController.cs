using MEDManager.Data;
using MEDManager.Models;
using MEDManager.ViewModel;
using MEDManager.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;


namespace MEDManager.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<Doctor> _userManager;

        private string? _doctorId => _userManager.GetUserId(User);

        public DashboardController(ApplicationDbContext dbContext, UserManager<Doctor> userManager)
        {
            _userManager = userManager;
            _dbContext = dbContext;
        }
        // GET: DashboardController
        public ActionResult Index()
        {
            MostMedicamentInPrescriptionStat();
            MostConsultedPatientsStat();
            MostMedicalHistoryInPatientStat();
            PatientsByAgeStat();
            MedicamentsCountStat();
            return View();
        }

        private void MostConsultedPatientsStat()
        {
            var mostConsultedPatients = _dbContext.Patients
                .Include(p => p.Prescriptions)
                .Where(p => p.DoctorId == _doctorId)
                .OrderByDescending(p => p.Prescriptions.Count).Take(5).ToList();

            var patientLabels = new List<string>();
            var patientData = new List<int>();

            foreach (var patient in mostConsultedPatients)
            {
                patientLabels.Add(patient.FirstName + " " + patient.LastName);
                patientData.Add(patient.Prescriptions.Count);
            }

            ViewBag.PatientLabels = patientLabels;
            ViewBag.PatientData = patientData;
        }

        private void MostMedicamentInPrescriptionStat()
        {
            var mostMedicamentInPrescription = _dbContext.Medicaments
                .Include(p => p.Prescriptions)
                .OrderByDescending(p => p.Prescriptions.Count).Take(5).ToList();

            var medicamentLabels = new List<string>();
            var medicamentData = new List<int>();

            foreach (var medicament in mostMedicamentInPrescription)
            {
                medicamentLabels.Add(medicament.Name);
                medicamentData.Add(medicament.Prescriptions.Count);
            }

            ViewBag.MedicamentLabels = medicamentLabels;
            ViewBag.MedicamentData = medicamentData;
        }

        private void MostMedicalHistoryInPatientStat()
        {
            var mostMedicalHistoryInPatientStat = _dbContext.MedicalHistories
                .Include(p => p.Patients)
                .OrderByDescending(p => p.Patients.Count).Take(5).ToList();

            var medicalHistoryLabels = new List<string>();
            var medicalHistoryData = new List<int>();

            foreach (var medicalHistory in mostMedicalHistoryInPatientStat)
            {
                medicalHistoryLabels.Add(medicalHistory.Name);
                medicalHistoryData.Add(medicalHistory.Patients.Count);
            }

            ViewBag.MedicalHistoryLabels = medicalHistoryLabels;
            ViewBag.MedicalHistoryData = medicalHistoryData;
        }

        private void PatientsByAgeStat()
        {
            var patientByAge = _dbContext.Patients.Where(p => p.DoctorId == _doctorId)
                .GroupBy(p => p.Age)
                .Select(g => new
                {
                    Age = g.Key,
                    Count = g.Count()
                }).ToList();

            var patientByAgeLabels = new List<string>();
            var patientByAgeData = new List<int>();

            for (int i = 0; i < 5; i++)
            {
                var min = i * 20;
                var max = (i + 1) * 20;

                var label = $"{min}-{max} ans";
                var count = patientByAge.Where(p => p.Age >= min && p.Age < max).Sum(p => p.Count);

                patientByAgeLabels.Add(label);
                patientByAgeData.Add(count);
            }

            ViewBag.PatientByAgeLabels = patientByAgeLabels;
            ViewBag.PatientByAgeData = patientByAgeData;
        }
        private void MedicamentsCountStat()
        {
            var medicamentsCountStat = _dbContext.Medicaments
                .ToList();

            var medicamentCountLabels = new List<string>();
            var medicamentCountData = new List<int>();


            medicamentCountLabels.Add("nombre de médicaments");
            medicamentCountData.Add(medicamentsCountStat.Count());

            ViewBag.MedicamentCountLabels = medicamentCountLabels;
            ViewBag.MedicamentCountData = medicamentCountData;
        }
    }
}
