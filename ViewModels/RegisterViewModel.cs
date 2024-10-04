using System;
using System.ComponentModel.DataAnnotations;

namespace MEDManager.Models;

public class RegisterViewModel
{
    [Required]
    [Display(Name = "User Name")]
    public string? UserName { get; set; }

    [DataType(DataType.Password)]
    [Required]
    [Display(Name = "Password")]
    public string? Password { get; set; }

    [Required]
    [Display(Name = "Prénom")]
    public required string FirstName { get; set; }

    [Required]
    [Display(Name = "Nom")]
    public required string LastName { get; set; }

    [Required]
    [Display(Name = "Email")]
    public required string Email { get; set; }
}
