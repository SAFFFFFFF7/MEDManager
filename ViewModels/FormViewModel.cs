using System.ComponentModel.DataAnnotations;

namespace MEDManager.ViewModel;

public class FormViewModel
{
    public int PrescriptionId { get; set; }
    
    [Display(Name = "Date de début")]
    [Required(ErrorMessage = "La date de début est requis.")]
    public DateOnly? StartDate { get; set; }
    
    [Display(Name = "Date de Fin")]    
    [Required(ErrorMessage = "La date de fin est requis.")]
    public DateOnly? EndDate { get; set; }
    
    [Display(Name = "Posologie")] 
    [StringLength(256, MinimumLength = 0, ErrorMessage = "La posologie doit contenir moins de 256 caractères.")]
    public string? Dosage { get; set; }
    
    [Display(Name = "Informations supplémentaires")] 
    [StringLength(2048, MinimumLength = 0, ErrorMessage = "La posologie doit contenir moins de 2048 caractères.")]
    public string? AdditionalInformation { get; set; }
    
    public List<Models.Medicament> Medicaments { get; set; } = new();
    public List<int> SelectedMedicamentIds { get; set; } = new();
}