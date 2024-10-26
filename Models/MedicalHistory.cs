using System;
using System.ComponentModel.DataAnnotations;

namespace MEDManager.Models;
public class MedicalHistory
{
    [Display(Name = "MedicalHistory Id")]
    public int Id { get; set; }

    [Display(Name = "Nom de l'antécédent médical")]
    [Required(ErrorMessage = "Le nom de l'antécédent médical est requis.")]
    [StringLength(256, MinimumLength = 1, ErrorMessage = "Le nom de l'antécédent médical doit contenir moins de 256 caractères.")]
    public string? Name { get; set; }
    public List<Patient> Patients { get; set; } = new();
    public List<Medicament> Medicaments { get; set; } = new();

}