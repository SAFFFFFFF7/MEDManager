using System;
using System.ComponentModel.DataAnnotations;

namespace MEDManager.Models;
public class Allergy
{
    [Key]
    public int Id { get; set; }
    public string? Name { get; set; }
    public List<Patient> Patients { get; set; } = new();
    public List<Medicament> Medicaments { get; set; } = new();

}