using System;
using System.ComponentModel.DataAnnotations;

namespace MEDManager.Models;

public class Medicament
{
    [Key]
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Quantity { get; set; }
    public string? Ingredients { get; set; }
    public List<Prescription> Prescriptions { get; set; } = new();
    public List<Allergy> Allergies { get; set; } = new();
    public List<MedicalHistory> MedicalHistories { get; set; } = new();
}