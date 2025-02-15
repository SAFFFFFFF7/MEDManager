using MEDManager.Data;
using MEDManager.Models;
using MEDManager.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace MEDManager.Controllers
{
    [Authorize]
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
                DrpMedicalHistories = _dbContext.MedicalHistories.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList(),
                DrpAllergies = _dbContext.Allergies.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Add(MedicamentViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.DrpMedicalHistories = _dbContext.MedicalHistories.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
                viewModel.DrpAllergies = _dbContext.Allergies.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
                return View(viewModel);
            }

            Medicament medicament = new Medicament
            {
                Name = viewModel.Name,
                Quantity = viewModel.Quantity,
                Ingredients = viewModel.Ingredients
            };

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
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                Medicament? medicamentToDelete = await _dbContext.Medicaments.Where(p => p.Id == id).FirstOrDefaultAsync();
                if (medicamentToDelete == null) return NotFound();

                _dbContext.Medicaments.Remove(medicamentToDelete);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction("Index", "Medicament");
            }
            catch (DbUpdateException ex)
            {
                return RedirectToAction("Index", "Medicament");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Medicament");
            }
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
                SelectedAllergyIds = medicament.Allergies.Select(a => a.Id).ToList() ?? new List<int>(),
                DrpMedicalHistories = _dbContext.MedicalHistories.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList(),
                DrpAllergies = _dbContext.Allergies.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList()
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
            viewModel.DrpMedicalHistories = _dbContext.MedicalHistories.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
            viewModel.DrpAllergies = _dbContext.Allergies.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
            return View(viewModel);
        }

        private bool MedicamentExists(int id)
        {
            return _dbContext.Medicaments.Any(m => m.Id == id);
        }
    }
}