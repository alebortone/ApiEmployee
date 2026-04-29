using System;
using System.Collections.Generic;
using System.Text;

namespace MinhaApi.Email.App
{
    public  interface IEmailService
    {
        Task SendEmail(EmployeeEmailModel emailModel, byte[] pdf);
    }
}
