using iText.Bouncycastleconnector;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using MEDManager.Models;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
namespace MEDManager.Services;

public static class PDFService
{
    public static void GeneratePrescriptionPdf(string filePath, Prescription prescription)
    {
        using var writer = new PdfWriter(filePath);
        using var pdf = new PdfDocument(writer);
        
        var document = new Document(pdf);

        document.Add(new Paragraph("Ordonnance")
            .SetTextAlignment(TextAlignment.CENTER)
            .SetFontSize(20)
            .SetBold()
            .SetMarginBottom(20));

        document.Add(new Paragraph($"Docteur : {prescription.Doctor.FullName}")
            .SetFontSize(12)
            .SetMarginBottom(5));
        document.Add(new Paragraph($"Contact : {prescription.Doctor.Email}")
            .SetFontSize(12)
            .SetMarginBottom(20));

        document.Add(new Paragraph($"Patient : {prescription.Patient.FirstName} {prescription.Patient.LastName}")
            .SetFontSize(12)
            .SetMarginBottom(5));
        document.Add(new Paragraph($"Âge : {prescription.Patient.Age}")
            .SetFontSize(12)
            .SetMarginBottom(20));

        document.Add(new Paragraph($"Date de début : {prescription.StartDate?.ToString("dd/MM/yyyy") ?? "Non spécifiée"}")
            .SetFontSize(12)
            .SetMarginBottom(5));
        document.Add(new Paragraph($"Date de fin : {prescription.EndDate?.ToString("dd/MM/yyyy") ?? "Non spécifiée"}")
            .SetFontSize(12)
            .SetMarginBottom(20));

        document.Add(new Paragraph("Médicaments :")
            .SetFontSize(14)
            .SetBold()
            .SetMarginBottom(10));

        if (prescription.Medicaments.Any())
        {
            foreach (var medicament in prescription.Medicaments)
            {
                document.Add(new Paragraph($"- {medicament.Name} : {medicament.Quantity}")
                    .SetFontSize(12)
                    .SetMarginBottom(5));
            }
        }

        if (!string.IsNullOrEmpty(prescription.AdditionalInformation))
        {
            document.Add(new Paragraph("Informations supplémentaires :")
                .SetFontSize(14)
                .SetBold()
                .SetMarginBottom(10));
            document.Add(new Paragraph(prescription.AdditionalInformation)
                .SetFontSize(12)
                .SetMarginBottom(20));
        }
        
        if (!string.IsNullOrEmpty(prescription.Dosage))
        {
            document.Add(new Paragraph("Posologie :")
                .SetFontSize(14)
                .SetBold()
                .SetMarginBottom(10));
            document.Add(new Paragraph(prescription.Dosage)
                .SetFontSize(12)
                .SetMarginBottom(20));
        }

        document.Close();
    }
}