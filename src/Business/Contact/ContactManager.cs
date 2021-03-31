namespace DetroitHarps.Business.Contact
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AutoMapper;
    using DetroitHarps.Business.Common.Constants;
    using DetroitHarps.Business.Contact.Entities;
    using DetroitHarps.Business.Contact.Models;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using Tools;

    public class ContactManager : IContactManager
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IMessageStatusRepository _messageStatusRepository;
        private readonly IEmailSender _emailSender;
        private readonly ILogger<ContactManager> _logger;

        public ContactManager(
            IMessageRepository messageRepository,
            IMessageStatusRepository messageStatusRepository,
            IEmailSender emailSender,
            ILogger<ContactManager> logger)
        {
            Guard.NotNull(messageRepository, nameof(messageRepository));
            Guard.NotNull(messageStatusRepository, nameof(messageStatusRepository));
            Guard.NotNull(emailSender, nameof(emailSender));
            Guard.NotNull(logger, nameof(logger));

            _messageRepository = messageRepository;
            _messageStatusRepository = messageStatusRepository;
            _emailSender = emailSender;
            _logger = logger;
        }

        public void Contact(MessageModel model)
        {
            Guard.NotNull(model, nameof(model), Constants.NullExceptionGenerator);

            _logger.LogInformation($"new message: {JsonConvert.SerializeObject(model)}");

            var subject = FormatSubject(model);
            var body = FormatBody(model);

            try
            {
                _emailSender.SendToSelf(subject, body);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "error sending email");
            }

            var entity = Mapper.Map<Message>(model);
            _messageRepository.Create(entity);
            _messageStatusRepository.SetAsUnread(entity.Id);
        }

        public void MarkAsRead(Guid id)
        {
            _logger.LogInformation($"marking message with id {id} as read");
            _messageStatusRepository.SetAsRead(id);
        }

        public void MarkAsUnread(Guid id)
        {
            _logger.LogInformation($"marking message with id {id} as unread");
            _messageStatusRepository.SetAsUnread(id);
        }

        public IEnumerable<MessageReadModel> GetAll()
        {
            var unreadMessageIds = _messageStatusRepository.GetUnreadMessageIds();
            return _messageRepository.GetAll()
                .Select(
                    x => Mapper.Map<Message, MessageReadModel>(
                        x,
                        opt => opt.AfterMap(
                            (src, dest) =>
                            {
                                dest.IsRead = !unreadMessageIds.Contains(dest.Id);
                            })));
        }

        private string FormatSubject(MessageModel model) => $"Message from {model.FirstName} {model.LastName}";

        private string FormatBody(MessageModel model)
        {
            var sb = new StringBuilder();

            sb.AppendLine($"Name: {model.FirstName} {model.LastName}");
            sb.AppendLine($"Email: {model.Email}");
            sb.AppendLine($"Body:");
            sb.Append(model.Body);
            return sb.ToString();
        }
    }
}