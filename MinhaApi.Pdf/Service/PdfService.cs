
using MinhaApi.Pdf.Interface;
using MinhaApi.Pdf.PdfModel;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace MinhaApi.Pdf.Service
{    
    
    public class PdfService : IPdfService
    {

        public byte[] GerarPdf(EmployeePdfModel employee)
        {

            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(20);
                    page.Size(PageSizes.A4);
                    page.DefaultTextStyle(x => x.FontSize(16));


                    page.Header()
                        .Text($"Hello {employee.Name}!")
                        .SemiBold().FontSize(36);

                    page.Content().Column(col =>
                    {

                        col.Item().PaddingTop(20);
                        col.Item().Text($"Nome: {employee.Name}");
                        col.Item().Text($"Idade: {employee.Age}");
                        col.Item().Text("Foto do funcionario: ");
                        if (employee.Photo != null)
                        {
                            col.Item()
                                .Width(150)
                                .Image(employee.Photo);

                        }
                        else
                        {
                            col.Item().Text("Funcionario não possui foto").Bold().FontSize(20);
                        }
                    });
                });
            });
            return document.GeneratePdf();
            
        }
    }
}
