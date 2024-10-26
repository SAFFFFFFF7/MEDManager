using MEDManager.Data;
using MEDManager.Models;
using MEDManager.ViewModel.Medicament;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MEDManager.Controllers
{
    public class MedicamentController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public MedicamentController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        // GET: MedicamentController
        public ActionResult Index()
        {
            return View(_dbContext.Medicaments);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var viewModel = new MedicamentViewModel
            {
                MedicalHistories = await _dbContext.MedicalHistories.ToListAsync(),
                Allergies = await _dbContext.Allergies.ToListAsync(),
                SelectedMedicalHistoryIds = new List<int>(),
                SelectedAllergyIds = new List<int>(),
            };

            return View(viewModel);
        }

                [HttpPost]
        public async Task<IActionResult> Add(Medicament medicament, MedicamentViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (viewModel.SelectedAllergyIds != null)
            {
                var selectedAllergies = await _dbContext.Allergies
                    .Where(a => viewModel.SelectedAllergyIds.Contains(a.Id))
                    .ToListAsync();
                foreach (var allergy in selectedAllergies)
                {
                    medicament.Allergies.Add(allergy);
                }
            }

            if (viewModel.SelectedMedicalHistoryIds != null)
            {
                var selectedMedicalHistories = await _dbContext.MedicalHistories
                    .Where(a => viewModel.SelectedMedicalHistoryIds.Contains(a.Id))
                    .ToListAsync();
                foreach (var medicalHistories in selectedMedicalHistories)
                {
                    medicament.MedicalHistories.Add(medicalHistories);
                }
            }
            _dbContext.Medicaments.Add(medicament);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            Medicament? medi = _dbContext.Medicaments.FirstOrDefault<Medicament>(s => s.Id == id);

            if (medi != null)
            {
                return View(medi);
            }

            return NotFound();
        }

        [HttpPost]

        public IActionResult DeleteConfirmed(int Id)
        {
            Medicament? medi = _dbContext.Medicaments.FirstOrDefault<Medicament>(m => m.Id == Id);

            if (medi != null)
            {
                _dbContext.Medicaments.Remove(medi);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return NotFound();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Medicament? medi = _dbContext.Medicaments.FirstOrDefault<Medicament>(m => m.Id == id);

            if (medi != null)
            {
                return View(medi);
            }

            return NotFound();
        }

        [HttpPost]
        public IActionResult Edit(Medicament medicament)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            Medicament? medi = _dbContext.Medicaments.FirstOrDefault<Medicament>(m => m.Id == medicament.Id);

            if (medi != null)
            {
                medi.Name = medicament.Name;
                medi.Quantity = medicament.Quantity;
                medi.Ingredients = medicament.Ingredients;

                _dbContext.SaveChanges();

                return RedirectToAction("Index");
            }

            return NotFound();
        }
    }
}