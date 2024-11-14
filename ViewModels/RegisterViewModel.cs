using System;
using System.ComponentModel.DataAnnotations;

namespace MEDManager.Models;

public class RegisterViewModel
{
    [Display(Name = "Nom d'utilisateur")]
    [Required(ErrorMessage = "Le Nom d'utilisateur est requis")]
    public string? UserName { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Mot de passe")]
    [Required(ErrorMessage = "Le mot de passe est requis")]
    public string? Password { get; set; }

    [Display(Name = "Prénom")]
    [Required(ErrorMessage = "Le prénom est requis")]
    public required string FirstName { get; set; }

    [Display(Name = "Nom")]
    [Required(ErrorMessage = "Le Nom est requis")]
    public required string LastName { get; set; }

    [Display(Name = "Email")]
    [Required(ErrorMessage = "L'email est requis")]
    public required string Email { get; set; }
}
