using MEDManager.Data;
using MEDManager.Models;
using MEDManager.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
            return View(_dbContext.Prescriptions.Where(p => p.DoctorId == _doctorId).ToList());
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var viewModel = new PrescriptionViewModel
            {
                DoctorId = _userManager.GetUserId(User),
                PatientId = _userManager.GetUserId(User),
                SelectedPatientIds = new List<int>(),
                SelectedMedicamentIds = new List<int>(),
                DrpPatients = _dbContext.Patients.Select(x => new SelectListItem { Text = x.LastName, Value = x.Id.ToString() }).ToList(),
                DrpMedicaments = _dbContext.Medicaments.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList()
            };

            return View(viewModel);
        }
    }
}
