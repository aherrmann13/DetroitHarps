namespace Business.Managers
{
    using System.Text;
    using Business.Abstractions;
    using Business.Models;
    using Tools;

    public class ContactManager : IContactManager
    {
        private readonly IEmailSender _emailSender;

        public ContactManager(IEmailSender emailSender)
        {
            Guard.NotNull(emailSender, nameof(emailSender));

            _emailSender = emailSender;
        }

        public void Contact(ContactModel model)
        {
            Guard.NotNull(model, nameof(model));

            var subject = FormatSubject(model);
            var body = FormatBody(model);
            _emailSender.SendToSelf(subject, body);
        }

        private string FormatSubject(ContactModel model)
        {
            return $"Message from {model.Name}";
        }

        private string FormatBody(ContactModel model)
        {
            var sb = new StringBuilder();

            sb.AppendLine($"Name: {model.Name}");
            sb.AppendLine($"Email: {model.Email}");
            sb.AppendLine($"Body:");
            sb.Append(model.Body);

            return sb.ToString();
        }
    }
}