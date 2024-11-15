using System;
using System.ComponentModel.DataAnnotations;
using MEDManager.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MEDManager.ViewModels;

public class PrescriptionViewModel
{
    public int PrescriptionId { get; set; }
    public Patient? Patient { get; set; }

    [Display(Name = "Date de début")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
    [Required(ErrorMessage = "La date de début est requis.")]
    public DateOnly? StartDate { get; set; }

    [Display(Name = "Date de Fin")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
    [Required(ErrorMessage = "La date de fin est requis.")]
    public DateOnly? EndDate { get; set; }

    [Display(Name = "Posologie")]
    [StringLength(256, MinimumLength = 0, ErrorMessage = "La posologie doit contenir moins de 256 caractères.")]
    public string? Dosage { get; set; }

    [Display(Name = "Informations supplémentaires")]
    [StringLength(2048, MinimumLength = 0, ErrorMessage = "La posologie doit contenir moins de 2048 caractères.")]
    public string? AdditionalInformation { get; set; }

    public List<Medicament> MedicamentsPatient { get; set; } = new();
    public List<int> SelectedMedicamentsPatientlIds { get; set; } = new();
    public List<SelectListItem> DrpMedicamentsPatient { get; set; } = new();
}