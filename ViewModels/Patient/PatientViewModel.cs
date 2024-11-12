using System.ComponentModel.DataAnnotations;
using MEDManager.Data;
using MEDManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MEDManager.ViewModels;

public class PatientViewModel
{
    public int PatientId { get; set; } = 0;

    [Display(Name = "Prénom")]
    [Required(ErrorMessage = "Le prénom est requis.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Le prénom doit contenir entre 2 et 100 caractères.")]
    public string FirstName { get; set; }

    [Display(Name = "Nom de famille")]
    [Required(ErrorMessage = "Le nom de famille est requis.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Le nom de famille doit contenir entre 2 et 100 caractères.")]
    public string LastName { get; set; }

    [Display(Name = "Âge")]
    [Required(ErrorMessage = "L'âge est requis.")]
    public int? Age { get; set; }

    [Display(Name = "Genre")]
    [Required(ErrorMessage = "Le genre du patient est requis.")]
    public Gender Gender { get; set; }

    [Display(Name = "Taille")]
    [Required(ErrorMessage = "La taille du patient est requis.")]
    [Range(typeof(int), "0", "300", ErrorMessage = "La taille du patient doit être comprise entre 0 et 300 cm.")]
    public int? Height { get; set; }

    [Display(Name = "Poids")]
    [Required(ErrorMessage = "Le poids du patient est requis.")]
    [Range(typeof(int), "0", "300", ErrorMessage = "Le poids du patient doit être compris entre 0 et 300 kg.")]
    public int Weight { get; set; }
    
    [Display(Name = "Numéro de sécurité social")]
    [Required(ErrorMessage = "Le numéro de sécurité social est obligatoire.")]
    // [Range(typeof(int), "0", "300", ErrorMessage = "Le poids du patient doit être compris entre 0 et 300 kg.")]
    public int? SecurityCardNumber { get; set; }
    public List<MedicalHistory>? MedicalHistories { get; set; }
    public List<Allergy>? Allergies { get; set; }

    [Display(Name = "Liste des antécédents médicaux")]
    public List<int> SelectedMedicalHistoryIds { get; set; } = new List<int>();

    [Display(Name = "liste des allergies")]
    public List<int> SelectedAllergyIds { get; set; } = new List<int>();

    public List<SelectListItem> DrpMedicalHistories { get; set; } = new();

    public List<SelectListItem> DrpAllergies { get; set; } = new();
}