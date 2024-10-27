using System;
using MEDManager.Models;

namespace MEDManager.ViewModels;

public class PatientListViewModel
{
    public int PatientId { get; set; }
    public List<Patient> Patients { get; set;} = new();
}
