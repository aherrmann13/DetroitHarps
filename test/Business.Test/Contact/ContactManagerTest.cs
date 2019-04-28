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
        private readonly Mock<IMessageRepository> _repositoryMock;
        private readonly Mock<IEmailSender> _emailSenderMock;
        private readonly Mock<ILogger<ContactManager>> _loggerMock;

        public ContactManagerTest()
        {
            _repositoryMock = new Mock<IMessageRepository>();
            _emailSenderMock = new Mock<IEmailSender>();
            _loggerMock = new Mock<ILogger<ContactManager>>();
        }

        [Fact]
        public void NullRepositoryInConstructorThrowsTestTest()
        {
            Assert.Throws<ArgumentNullException>(() => new ContactManager(null, _emailSenderMock.Object, _loggerMock.Object));
        }

        [Fact]
        public void NullEmailSenderInConstructorThrowsTestTest()
        {
            Assert.Throws<ArgumentNullException>(() => new ContactManager(_repositoryMock.Object, null, _loggerMock.Object));
        }

        [Fact]
        public void NullLoggerInConstructorThrowsTestTest()
        {
            Assert.Throws<ArgumentNullException>(() => new ContactManager(_repositoryMock.Object, _emailSenderMock.Object, null));
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

            _repositoryMock.Verify(
                x => x.Create(It.Is<Message>(y => y != null)),
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
            _repositoryMock.Verify(
                x => x.Create(It.Is<Message>(y => y != null)),
                Times.Once);
        }

        [Fact]
        public void MarkAsReadThrowsWhenNonExistantTest()
        {
            var manager = GetManager();
            _repositoryMock.Setup(x => x.GetSingleOrDefault(It.IsAny<int>())).Returns((Message)null);

            Assert.Throws<BusinessException>(() => manager.MarkAsRead(1));
        }

        [Fact]
        public void MarkAsReadDoesNotUpdateWhenAlreadyReadTest()
        {
            var manager = GetManager();
            _repositoryMock.Setup(x => x.GetSingleOrDefault(It.IsAny<int>())).Returns(new Message { IsRead = true });

            manager.MarkAsRead(1);

            _repositoryMock.Verify(x => x.Update(It.IsAny<Message>()), Times.Never);
        }

        [Fact]
        public void MarkAsReadUpdatesWhenNotReadTest()
        {
            var manager = GetManager();
            var message = new Message { IsRead = false };
            _repositoryMock.Setup(x => x.GetSingleOrDefault(It.IsAny<int>())).Returns(message);

            manager.MarkAsRead(1);

            _repositoryMock
                .Verify(x => x.Update(It.Is<Message>(y => y.Equals(message) && y.IsRead)), Times.Once);
        }

        [Fact]
        public void MarkAsUnReadThrowsWhenNonExistantTest()
        {
            var manager = GetManager();
            _repositoryMock.Setup(x => x.GetSingleOrDefault(It.IsAny<int>())).Returns((Message)null);

            Assert.Throws<BusinessException>(() => manager.MarkAsUnread(1));
        }

        [Fact]
        public void MarkAsUnreadDoesNotUpdateWhenAlreadyUnreadTest()
        {
            var manager = GetManager();
            _repositoryMock.Setup(x => x.GetSingleOrDefault(It.IsAny<int>())).Returns(new Message { IsRead = false });

            manager.MarkAsUnread(1);

            _repositoryMock.Verify(x => x.Update(It.IsAny<Message>()), Times.Never);
        }

        [Fact]
        public void MarkAsUnreadUpdatesWhenReadTest()
        {
            var manager = GetManager();
            var message = new Message { IsRead = true };
            _repositoryMock.Setup(x => x.GetSingleOrDefault(It.IsAny<int>())).Returns(message);

            manager.MarkAsUnread(1);

            _repositoryMock
                .Verify(x => x.Update(It.Is<Message>(y => y.Equals(message) && !y.IsRead)), Times.Once);
        }

        [Fact]
        public void GetAllReturnsModelsTest()
        {
            var models = new List<Message>()
            {
                new Message(),
                new Message()
            };
            _repositoryMock.Setup(x => x.GetAll())
                .Returns(models);

            var manager = GetManager();

            var modelsFromManager = manager.GetAll();

            Assert.Equal(models.Count, modelsFromManager.Count());
            Assert.All(modelsFromManager, x => Assert.NotNull(x));
        }

        private ContactManager GetManager() =>
            new ContactManager(_repositoryMock.Object, _emailSenderMock.Object, _loggerMock.Object);
    }
}