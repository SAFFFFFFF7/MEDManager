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
            return View();
        }

        private void MostConsultedPatientsStat()
        {
            var mostConsultedPatients = _dbContext.Patients.Where(p => p.DoctorId == _doctorId)
                .OrderByDescending(p => p.Prescriptions.Count).Take(5).ToList();

            var patientsLabels = new List<string>();
            var patientsData = new List<int>();

            foreach (var patient in mostConsultedPatients)
            {
                patientsLabels.Add(patient.FirstName + " " + patient.LastName);
                patientsData.Add(patient.Prescriptions.Count);
            }

            ViewBag.PatientsLabels = patientsLabels;
            ViewBag.PatientsData = patientsData;
        }

        private void MostMedicamentInPrescriptionStat()
        {
            var mostMedicamentInPrescription = _dbContext.Medicaments
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
    }
}
