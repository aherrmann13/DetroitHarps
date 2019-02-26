namespace DetroitHarps.Api.Services
{
    using System;
    using DetroitHarps.Business.Contact;
    using MailKit;
    using MailKit.Net.Smtp;
    using MimeKit;
    using Tools;

    // https://github.com/jstedfast/MailKit
    public class EmailSender : IEmailSender
    {
        private const string SmtpHost = "smtp.gmail.com";
        private const int SmtpPort = 587;
        private const string TextPartSubtype = "plain";

        private readonly EmailSettings _settings;

        public EmailSender(EmailSettings settings)
        {
            Guard.NotNull(settings, nameof(settings));

            _settings = settings;
        }

        public void SendToSelf(string subject, string body)
        {
            using (var client = new SmtpClient())
            {
                // For demo-purposes, accept all SSL certificates (in case the server supports STARTTLS)
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                client.Connect(SmtpHost, SmtpPort, useSsl: false);

                client.Authenticate(_settings.Email, _settings.Password);

                client.Send(CreateMessage(subject, body));
                client.Disconnect(true);
            }
        }

        private MimeMessage CreateMessage(string subject, string body)
        {
            var address = new MailboxAddress(_settings.Email);
            var message = new MimeMessage
            {
                Subject = subject,
                Body = new TextPart(TextPartSubtype)
                {
                    Text = body
                }
            };
            message.From.Add(address);
            message.To.Add(address);

            return message;
        }
    }
}