namespace MinhaApi.Pdf.PdfModel
{
    public record EmployeePdfModel(string Name, int Age, byte[]? Photo = null)
    {
    }
}
