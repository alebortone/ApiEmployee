using Microsoft.Extensions.Options;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
using MinhaApi.Email.App;

namespace MinhaApi.Email.Service
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _settings;

        public EmailService(IOptions<EmailSettings> settings)
        {
            _settings = settings.Value;
        }


        public async Task SendEmail(EmployeeEmailModel emailModel, byte[] pdfBytes)
        {

            var message = new MimeMessage();

            message.From.Add(new MailboxAddress(_settings.Name, _settings.Email));
            Console.WriteLine($"EMAIL: '{emailModel.Email}'");
            message.To.Add(new MailboxAddress($"{emailModel.Name}", $"{emailModel.Email}"));
            message.Subject = "Seus dados cadastrados";

            var builder = new BodyBuilder();


            builder.Attachments.Add($"funcionario{emailModel.Name}.pdf", pdfBytes);

            message.Body = builder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                client.Connect(_settings.Smtp, _settings.Port, false);

                client.Authenticate(_settings.Email, _settings.Password);

                client.Send(message);
                client.Disconnect(true);
            }
        }


    }
}
