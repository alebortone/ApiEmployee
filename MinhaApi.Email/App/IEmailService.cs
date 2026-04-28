using System;
using System.Collections.Generic;
using System.Text;

namespace MinhaApi.Email.App
{
    public  interface IEmailService
    {
        void SendEmail(EmployeeEmailModel emailModel, byte[] pdf);
    }
}
