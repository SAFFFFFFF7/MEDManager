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
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Le prénom doit contenir entre 2 et 100 caractères.")]
    public string? FirstName { get; set; }

    [Display(Name = "Last Name")]
    [Required(ErrorMessage = "Le nom est obligatoire.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Le nom de famille doit contenir entre 2 et 100 caractères.")]
    public string? LastName { get; set; }

    [Display(Name = "Âge")]
    [Required(ErrorMessage = "L'âge est obligatoire.")]
    public int? Age { get; set; }

    [Display(Name = "Genre")]
    [Required(ErrorMessage = "Le genre est obligatoire.")]
    public Gender? Gender { get; set; }

    [Display(Name = "Taille")]
    [Required(ErrorMessage = "La taille est obligatoire.")]
    [Range(typeof(int), "0", "300", ErrorMessage = "La taille du patient doit être comprise entre 0 et 300 cm.")]
    public int? Height { get; set; }

    [Display(Name = "Poids")]
    [Required(ErrorMessage = "Le poids est obligatoire.")]
    [Range(typeof(int), "0", "300", ErrorMessage = "Le poids du patient doit être compris entre 0 et 300 kg.")]
    public int? Weight { get; set; }

    [Display(Name = "Numéro de sécurité social")]
    [Required(ErrorMessage = "Le numéro de sécurité social est obligatoire.")]
    public int? SecurityCardNumber { get; set; }
    public required string DoctorId { get; set; }
    public Doctor? Doctor { get; set; }
    public List<Prescription> Prescriptions { get; set; } = new();
    public List<Allergy> Allergies { get; set; } = new();
    public List<MedicalHistory> MedicalHistories { get; set; } = new();
}