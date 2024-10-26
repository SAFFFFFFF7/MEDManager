using System;
using System.ComponentModel.DataAnnotations;

namespace MEDManager.Models;

public class Medicament
{
    [Display(Name = "Médicament Id")]
    public int Id { get; set; }
    
    [Display(Name = "Nom du médicament")]
    [Required(ErrorMessage = "Le nom du médicament est requis.")]
    [StringLength(256, MinimumLength = 1, ErrorMessage = "Le nom du médicament doit contenir moins de 256 caractères.")]
    public required string Name { get; set; }
    
    [Display(Name = "Quantité du médicament")]
    [Required(ErrorMessage = "La quantité est requise.")]
    [StringLength(1024, MinimumLength = 1, ErrorMessage = "Le quantité du médicament doit contenir moins de 1024 caractères.")]
    public required string Quantity { get; set; }
    
    [Display(Name = "Ingrédients du médicament")]
    [Required(ErrorMessage = "Les ingrédients sont requis.")]
    [StringLength(1024, MinimumLength = 1, ErrorMessage = "Les ingrédients du médicament doivent contenir moins de 1024 caractères.")]
    public required string Ingredients { get; set; }

    public List<Prescription> Prescriptions { get; set; } = new();
    public List<Allergy> Allergies { get; set; } = new();
    public List<MedicalHistory> MedicalHistories { get; set; } = new();
}