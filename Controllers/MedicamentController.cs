using MEDManager.Data;
using MEDManager.Models;
using MEDManager.ViewModel;
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

        [HttpGet]
        public async Task<IActionResult> Find(string searchString)
        {
            if (_dbContext.Medicaments == null)
            {
                return Problem("Entity set 'MvcMovieContext.Movie'  is null.");
            }

            List<Medicament> medicaments = new();

            if (!String.IsNullOrEmpty(searchString))
            {
                medicaments = _dbContext.Medicaments.Where(s => s.Name!.ToUpper().Contains(searchString.ToUpper())).ToList();
            }

            return View("Index", medicaments);
        }

        [HttpPost]
        public string Find(string searchString, bool notUsed)
        {
            return "From [HttpPost]Index: filter on " + searchString;
        }

        // GET: MedicamentController
        public ActionResult Index()
        {
            return View(_dbContext.Medicaments);
        }

        [HttpGet]
        public async Task<IActionResult> ShowDetails(int id)
        {
            var medicament = await _dbContext.Medicaments
                .Include(p => p.MedicalHistories)
                .Include(p => p.Allergies)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (medicament != null)
            {
                return View(medicament);
            }

            return NotFound();
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
        public async Task<IActionResult> Edit(int id)
        {
            var medicament = await _dbContext.Medicaments
                .Include(p => p.MedicalHistories)
                .Include(p => p.Allergies)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (medicament == null)
            {
                return NotFound();
            }

            var viewModel = new MedicamentViewModel
            {
                MedicamentId = medicament.Id,
                Name = medicament.Name,
                Quantity = medicament.Quantity,
                Ingredients = medicament.Ingredients,
                MedicalHistories = await _dbContext.MedicalHistories.ToListAsync(),
                Allergies = await _dbContext.Allergies.ToListAsync(),
                SelectedMedicalHistoryIds = medicament.MedicalHistories.Select(m => m.Id).ToList() ?? new List<int>(),
                SelectedAllergyIds = medicament.Allergies.Select(a => a.Id).ToList() ?? new List<int>()
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, MedicamentViewModel viewModel)
        {
            if (id != viewModel.MedicamentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var medicament = await _dbContext.Medicaments
                        .Include(p => p.MedicalHistories)
                        .Include(p => p.Allergies)
                        .FirstOrDefaultAsync(p => p.Id == id);

                    if (medicament == null)
                    {
                        return NotFound();
                    }

                    medicament.Name = viewModel.Name;
                    medicament.Quantity = viewModel.Quantity;
                    medicament.Ingredients = viewModel.Ingredients;

                    medicament.Allergies.Clear();
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

                    medicament.MedicalHistories.Clear();
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

                    _dbContext.Entry(medicament).State = EntityState.Modified;
                    await _dbContext.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));

                }
                catch
                {
                    if (!MedicamentExists(viewModel.MedicamentId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            viewModel.MedicalHistories = await _dbContext.MedicalHistories.ToListAsync();
            viewModel.Allergies = await _dbContext.Allergies.ToListAsync();
            return View(viewModel);
        }

        private bool MedicamentExists(int id)
        {
            return _dbContext.Medicaments.Any(m => m.Id == id);
        }
    }
}