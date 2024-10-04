using MEDManager.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MEDManager.Data;

public class ApplicationDbContext : IdentityDbContext<Doctor>
{

  // Nous allons creer un dbset pour chaque table de notre base de donnees
  // Dbset est une classe generique qui represente une table dans la base de donnees
  // Elle est responsable du mapping objet-relationnel


  //TODO: Rename Allergys to Allergies TYPO WATCH OUT SAFWANE
  public DbSet<Allergy> Allergies => Set<Allergy>();
  public DbSet<MedicalHistory> MedicalHistories => Set<MedicalHistory>();
  public DbSet<Medicament> Medicaments => Set<Medicament>();
  public DbSet<Patient> Patients => Set<Patient>();
  public DbSet<Prescription> Prescriptions => Set<Prescription>();

  // Constructeur
  public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
  {
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<Allergy>()
      .HasMany(a => a.Patients)
      .WithMany(p => p.Allergies)
      .UsingEntity(j => j.ToTable("PatientAllergy"));


    modelBuilder.Entity<Allergy>()
      .HasMany(a => a.Medicaments)
      .WithMany(m => m.Allergies);

    modelBuilder.Entity<MedicalHistory>()
      .HasMany(m => m.Patients)
      .WithMany(p => p.MedicalHistories);

    modelBuilder.Entity<MedicalHistory>()
      .HasMany(m => m.Medicaments)
      .WithMany(m => m.MedicalHistories);

    modelBuilder.Entity<Doctor>()
        .HasMany(d => d.Patients)
        .WithOne(p => p.Doctor);

    modelBuilder.Entity<Doctor>()
        .HasMany(d => d.Prescriptions)
        .WithOne(p => p.Doctor);

    modelBuilder.Entity<Medicament>()
        .HasMany(m => m.Prescriptions)
        .WithMany(p => p.Medicaments);

    modelBuilder.Entity<Prescription>()
        .HasOne(p => p.Patient)
        .WithMany(p => p.Prescriptions);


    base.OnModelCreating(modelBuilder);
  }
}
