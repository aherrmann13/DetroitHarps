namespace DetroitHarps.Business.Test.Contact
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DetroitHarps.Business.Common.Exceptions;
    using DetroitHarps.Business.Contact;
    using DetroitHarps.Business.Contact.Entities;
    using DetroitHarps.Business.Contact.Models;
    using Microsoft.Extensions.Logging;
    using Moq;
    using Tools;
    using Xunit;

    [Collection("AutoMapper")]
    public class ContactManagerTest
    {
        private readonly Mock<IMessageRepository> _messageRepositoryMock;
        private readonly Mock<IMessageStatusRepository> _messageStatusRepositoryMock;
        private readonly Mock<IEmailSender> _emailSenderMock;
        private readonly Mock<ILogger<ContactManager>> _loggerMock;

        public ContactManagerTest()
        {
            _messageRepositoryMock = new Mock<IMessageRepository>();
            _messageStatusRepositoryMock = new Mock<IMessageStatusRepository>();
            _emailSenderMock = new Mock<IEmailSender>();
            _loggerMock = new Mock<ILogger<ContactManager>>();
        }

        [Fact]
        public void NullMessageRepositoryInConstructorThrowsTestTest()
        {
            Assert.Throws<ArgumentNullException>(
                () => new ContactManager(
                    null,
                    _messageStatusRepositoryMock.Object,
                    _emailSenderMock.Object,
                    _loggerMock.Object));
        }

        [Fact]
        public void NullMessageStatusRepositoryInConstructorThrowsTestTest()
        {
            Assert.Throws<ArgumentNullException>(
                () => new ContactManager(
                    _messageRepositoryMock.Object,
                    null,
                    _emailSenderMock.Object,
                    _loggerMock.Object));
        }

        [Fact]
        public void NullEmailSenderInConstructorThrowsTestTest()
        {
            Assert.Throws<ArgumentNullException>(
                () => new ContactManager(
                    _messageRepositoryMock.Object,
                    _messageStatusRepositoryMock.Object,
                    null,
                    _loggerMock.Object));
        }

        [Fact]
        public void NullLoggerInConstructorThrowsTestTest()
        {
            Assert.Throws<ArgumentNullException>(
                () => new ContactManager(
                    _messageRepositoryMock.Object,
                    _messageStatusRepositoryMock.Object,
                    _emailSenderMock.Object,
                    null));
        }

        [Fact]
        public void ContactNullModelThrowsTest()
        {
            var manager = GetManager();

            Assert.Throws<BusinessException>(() => manager.Contact(null));
        }

        [Fact]
        public void ContactCorrectSubjectTest()
        {
            var manager = GetManager();
            var model = new MessageModel
            {
                FirstName = Guid.NewGuid().ToString(),
                LastName = Guid.NewGuid().ToString(),
                Email = Guid.NewGuid().ToString(),
                Body = Guid.NewGuid().ToString()
            };

            manager.Contact(model);

            var expectedSubject = $"Message from {model.FirstName} {model.LastName}";

            _emailSenderMock.Verify(
                x => x.SendToSelf(It.Is<string>(y => Compare.EqualOrdinal(y, expectedSubject)), It.IsAny<string>()),
                Times.Once);
        }

        [Fact]
        public void ContactCorrectBodyTest()
        {
            var manager = GetManager();
            var model = new MessageModel
            {
                FirstName = Guid.NewGuid().ToString(),
                LastName = Guid.NewGuid().ToString(),
                Email = Guid.NewGuid().ToString(),
                Body = Guid.NewGuid().ToString()
            };

            manager.Contact(model);

            var expectedBody = $"Name: {model.FirstName} {model.LastName}{Environment.NewLine}" +
                                $"Email: {model.Email}{Environment.NewLine}" +
                                $"Body:{Environment.NewLine}" +
                                model.Body;

            _emailSenderMock.Verify(
                x => x.SendToSelf(It.IsAny<string>(), It.Is<string>(y => Tools.Compare.EqualOrdinal(y, expectedBody))),
                Times.Once);
        }

        [Fact]
        public void ContactPassesModelToRepositoryTest()
        {
            var manager = GetManager();
            var model = new MessageModel();

            manager.Contact(model);

            _messageRepositoryMock.Verify(
                x => x.Create(It.Is<Message>(y => y != null)),
                Times.Once);
        }

        [Fact]
        public void ContactSetsMessageAsUnreadTest()
        {
            var manager = GetManager();
            var model = new MessageModel();

            manager.Contact(model);

            _messageStatusRepositoryMock.Verify(
                x => x.SetAsUnread(It.Is<Guid>(y => y != default(Guid))),
                Times.Once);
        }

        [Fact]
        public void ContactHandlesEmailSenderExceptionTest()
        {
            var manager = GetManager();
            var model = new MessageModel();
            var ex = new Exception();
            _emailSenderMock
                .Setup(x => x.SendToSelf(It.IsAny<string>(), It.IsAny<string>()))
                .Throws(ex);

            manager.Contact(model);

            _loggerMock.Verify(
                x => x.Log<object>(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<object>(y =>
                        Compare.EqualOrdinal(y.ToString(), "error sending email")),
                    It.Is<Exception>(y => y.Equals(ex)),
                    It.IsAny<Func<object, Exception, string>>()),
                Times.Once());
            _messageRepositoryMock.Verify(
                x => x.Create(It.Is<Message>(y => y != null)),
                Times.Once);
        }

        [Fact]
        public void MarkAsReadMarksAsReadInMessageStatusRepositoryTest()
        {
            var id = Guid.NewGuid();
            var manager = GetManager();

            manager.MarkAsRead(id);

            _messageStatusRepositoryMock.Verify(x => x.SetAsRead(It.Is<Guid>(y => y.Equals(id))), Times.Once);
        }

        [Fact]
        public void MarkAsUnreadMarksAsUnreadInMessageStatusRepositoryTest()
        {
            var id = Guid.NewGuid();
            var manager = GetManager();

            manager.MarkAsUnread(id);

            _messageStatusRepositoryMock.Verify(x => x.SetAsUnread(It.Is<Guid>(y => y.Equals(id))), Times.Once);
        }

        [Fact]
        public void GetAllReturnsModelsTest()
        {
            var models = new List<Message>()
            {
                new Message { Id = Guid.NewGuid() },
                new Message { Id = Guid.NewGuid() }
            };
            var unreadIds = new List<Guid> { models[0].Id };
            _messageRepositoryMock.Setup(x => x.GetAll()).Returns(models);
            _messageStatusRepositoryMock.Setup(x => x.GetUnreadMessageIds()).Returns(unreadIds);

            var manager = GetManager();

            var modelsFromManager = manager.GetAll();

            Assert.Equal(models.Count, modelsFromManager.Count());
            Assert.All(modelsFromManager, x => Assert.NotNull(x));
            Assert.All(modelsFromManager.Where(x => unreadIds.Contains(x.Id)), x => Assert.False(x.IsRead));
            Assert.All(modelsFromManager.Where(x => !unreadIds.Contains(x.Id)), x => Assert.True(x.IsRead));
        }

        private ContactManager GetManager() =>
            new ContactManager(
                _messageRepositoryMock.Object,
                _messageStatusRepositoryMock.Object,
                _emailSenderMock.Object,
                _loggerMock.Object);
    }
}