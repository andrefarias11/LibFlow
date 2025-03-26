using LibFlow.Models;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace LibFlow.Services.Email
{
    public class SendEmail : ISendEmail
    {
        private readonly SmtpSettings _smtpSettings;
        private readonly IWebHostEnvironment _env;

        public SendEmail(IOptions<SmtpSettings> smtpSettings, IWebHostEnvironment env)
        {
            _smtpSettings = smtpSettings.Value;
            _env = env;
        }

        public async Task SendEmailAsync(string email, string subject, string body)
        {
            try
            {
                var message = new MimeMessage();

                message.From.Add(new MailboxAddress(_smtpSettings.SenderName,
                                                    _smtpSettings.SenderEmail));
                message.To.Add(new MailboxAddress("destino", email));
                message.Subject = subject;
                message.Body = new TextPart("html")
                {
                    Text = body
                };

                using (var client = new SmtpClient())
                {
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                    await client.ConnectAsync(_smtpSettings.SmtpServer, _smtpSettings.SmtpPort);

                    await client.AuthenticateAsync(_smtpSettings.SenderEmail, _smtpSettings.SenderPassword);

                    await client.SendAsync(message);

                    await client.DisconnectAsync(true);
                }
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }
        }
        public string LoadEmailTemplate(string bookName)
        {
            var templatePath = Path.Combine(Directory.GetCurrentDirectory(), "Templates", "EmailTemplate.html");

            if (!File.Exists(templatePath))
                throw new FileNotFoundException("Template de e-mail não encontrado!");

            var templateContent = File.ReadAllText(templatePath);
            return templateContent.Replace("{{BookName}}", bookName);
        }
    }
}
