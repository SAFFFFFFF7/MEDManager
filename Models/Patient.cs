using System;
using System.ComponentModel.DataAnnotations;

namespace MEDManager.Models;

public enum Gender { HOMME, FEMME, AUTRE }
public class Patient
{
    [Key]
    [Display(Name = "Patient Id")]
    public int Id { get; set; }

    [Display(Name = "First Name")]
    [Required(ErrorMessage = "Le prénom est obligatoire.")]
    public string? FirstName { get; set; }

    [Display(Name = "Last Name")]
    [Required(ErrorMessage = "Le nom est obligatoire.")]
    public string? LastName { get; set; }

    [Required(ErrorMessage = "L'âge est obligatoire.")]
    public int Age { get; set; }

    [Required(ErrorMessage = "Le genre est obligatoire.")]
    public Gender Gender { get; set; }

    [Required(ErrorMessage = "La taille est obligatoire.")]
    public int Height { get; set; }

    [Required(ErrorMessage = "Le poids est obligatoire.")]
    public double Weight { get; set; }
    public int DoctorId { get; set; }
    public Doctor Doctor { get; set; } = new();
    public List<Prescription> Prescriptions { get; set; } = new();
    public List<Allergy> Allergies { get; set; } = new();
    public List<MedicalHistory> MedicalHistories { get; set; } = new();
}