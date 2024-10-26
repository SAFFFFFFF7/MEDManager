using MEDManager.Data;
using MEDManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MEDManager.ViewModels;

public class PatientViewModel
{
    public Patient? Patient { get; set; }
    public required string DoctorId { get; set; }
    public List<MedicalHistory>? MedicalHistories { get; set; }
    public List<Allergy>? Allergies { get; set; }
    public List<int> SelectedMedicalHistoryIds { get; set; } = new List<int>();
    public List<int> SelectedAllergyIds { get; set; } = new List<int>();
    public List<SelectListItem> DrpMedicalHistories { get; set; } = new();
    public List<SelectListItem> DrpAllergies { get; set; } = new();
}