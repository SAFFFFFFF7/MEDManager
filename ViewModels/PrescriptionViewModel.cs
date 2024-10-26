using System;
using MEDManager.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MEDManager.ViewModels;

public class PrescriptionViewModel
{
    public Prescription? Prescription { get; set; }
    public required string DoctorId { get; set; }
    public required string PatientId { get; set; }
    public List<Patient> Patients { get; set; } = new();
    public List<Medicament> Medicaments { get; set; } = new();
    public List<int> SelectedPatientIds { get; set; } = new List<int>();
    public List<int> SelectedMedicamentIds { get; set; } = new List<int>();
    public List<SelectListItem> DrpPatients { get; set; } = new();
    public List<SelectListItem> DrpMedicaments { get; set; } = new();
}
