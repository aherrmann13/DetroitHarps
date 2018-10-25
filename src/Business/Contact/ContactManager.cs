namespace DetroitHarps.Business.Contact
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AutoMapper;
    using DetroitHarps.Business.Contact.Entities;
    using DetroitHarps.Business.Contact.Models;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using Tools;

    public class ContactManager : IContactManager
    {
        private readonly IMessageRepository _repository;
        private readonly IEmailSender _emailSender;
        private readonly ILogger<ContactManager> _logger;

        public ContactManager(
            IMessageRepository repository,
            IEmailSender emailSender,
            ILogger<ContactManager> logger)
        {
            Guard.NotNull(repository, nameof(repository));
            Guard.NotNull(emailSender, nameof(emailSender));
            Guard.NotNull(logger, nameof(logger));

            _repository = repository;
            _emailSender = emailSender;
            _logger = logger;
        }

        public void Contact(MessageModel model)
        {
            Guard.NotNull(model, nameof(model));

            _logger.LogInformation($"new message: {JsonConvert.SerializeObject(model)}");

            var subject = FormatSubject(model);
            var body = FormatBody(model);

            _emailSender.SendToSelf(subject, body);
            _repository.Create(Mapper.Map<Message>(model));
        }

        public void MarkAsRead(int id)
        {
            var message = GetMessageOrThrow(id);
            if(message.IsRead)
            {
                _logger.LogInformation($"message with id {id} is already marked as read");
            }
            else
            {
                _logger.LogInformation($"marking message with id {id} as read");
                message.IsRead = true;
                _repository.Update(message);
            }
        }

        public void MarkAsUnread(int id)
        {
            var message = GetMessageOrThrow(id);
            if(message.IsRead)
            {
                _logger.LogInformation($"marking message with id {id} as unread");
                message.IsRead = false;
                _repository.Update(message);
            }
            else
            {
                _logger.LogInformation($"message with id {id} is already marked as unread");
            }
        }

        public IEnumerable<MessageReadModel> GetAll() =>
            _repository.GetAll().Select(Mapper.Map<MessageReadModel>);

        private string FormatSubject(MessageModel model)
        {
            return $"Message from {model.FirstName} {model.LastName}";
        }

        private string FormatBody(MessageModel model)
        {
            var sb = new StringBuilder();

            sb.AppendLine($"Name: {model.FirstName} {model.LastName}");
            sb.AppendLine($"Email: {model.Email}");
            sb.AppendLine($"Body:");
            sb.Append(model.Body);

            return sb.ToString();
        }

        private Message GetMessageOrThrow(int id) =>
            _repository.GetSingleOrDefault(id) ?? throw new InvalidOperationException($"message with id: {id} does not exist");
    }
}