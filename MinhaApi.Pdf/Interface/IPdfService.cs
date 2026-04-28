
using MinhaApi.Pdf.PdfModel;

using System;
using System.Collections.Generic;
using System.Text;

namespace MinhaApi.Pdf.Interface
{
    public interface IPdfService
    {

        public byte[] GerarPdf(EmployeePdfModel employeeModel);
    }
}
