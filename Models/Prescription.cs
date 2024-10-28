using System;
using System.ComponentModel.DataAnnotations;

namespace MEDManager.Models;

public class Prescription
{
    [Key]
    public int Id { get; set; }
    public DateOnly? StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    public string? Dosage { get; set; }
    public string? AdditionalInformation { get; set; }
    public int PatientId { get; set; }
    public required Patient Patient { get; set; }
    public required string DoctorId { get; set; }
    public required Doctor Doctor { get; set; }
    public List<Medicament> Medicaments { get; set; } = new();
}