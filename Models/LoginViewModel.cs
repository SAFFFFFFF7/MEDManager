using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace MEDManager.Models;

public class LoginViewModel
{
    [Required (ErrorMessage = "The Username field is required.")]
    public string? UserName { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string? Password { get; set; }

    [Display(Name = "Remember me?")]
    public bool RememberMe { get; set; }
}
