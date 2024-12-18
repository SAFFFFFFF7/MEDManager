using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace MEDManager.Models;
public class Doctor : IdentityUser
{
    // [Key]
    // [Display (Name = "Doctor Id")]
    // public int Id { get; set; }

    [Display(Name = "First Name")]
    [Required(ErrorMessage = "Le prénom est obligatoire.")]
    public string? FirstName { get; set; }

    [Display(Name = "Last Name")]
    [Required(ErrorMessage = "Le nom est obligatoire.")]
    public string? LastName { get; set; }
    public List<Patient> Patients { get; set; } = new();
    public List<Prescription> Prescriptions { get; set; } = new();
    public string FullName => $"{FirstName} {LastName}";
    public static implicit operator Doctor?(string? v)
    {
        throw new NotImplementedException();
    }
}