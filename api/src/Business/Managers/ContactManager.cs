namespace Business.Managers
{
    using System;
    using System.Net;
    using System.Net.Mail;
    using Business.Interfaces;
    using Business.Models;
    using Stripe;
    using Tools;

    public class ContactManager : IContactManager
    {
        private readonly ContactManagerOptions _contactManagerOptions;

        public ContactManager(ContactManagerOptions contactManagerOptions)
        {
            Guard.NotNull(contactManagerOptions, nameof(contactManagerOptions));

            _contactManagerOptions = contactManagerOptions;
        }

        public void Contact(ContactModel model)
        {
            Guard.NotNull(model, nameof(model));

            var message = $"Message from: {model.Name}{Environment.NewLine}" +
                          $"Email Address: {model.Email}{Environment.NewLine}" +
                          $"Body:{Environment.NewLine}" +
                          $"{model.Message}{Environment.NewLine}";          
            Contact($"Message from {model.Name}", message);
        }

        public void Contact(string subject, string message)
        {
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential(_contactManagerOptions.FromEmail, _contactManagerOptions.Password),
                EnableSsl = true
            };
       
            client.Send(_contactManagerOptions.FromEmail, _contactManagerOptions.ToEmail, subject, message);
        }
    }
}