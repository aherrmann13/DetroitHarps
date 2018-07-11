namespace Business.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Business.Entities;
    using Business.Abstractions;
    using Business.Managers;
    using Business.Models;
    using Moq;
    using Xunit;
    using Tools;

    [Collection("AutoMapper")]
    public class ContactManagerTest
    {
        private readonly Mock<IEmailSender> _emailSenderMock;
        
        public ContactManagerTest()
        {
            _emailSenderMock = new Mock<IEmailSender>();
        }

        [Fact]
        public void NullEmailSenderInConstructorThrowsTestTest()
        {
            Assert.Throws<ArgumentNullException>(() => new ContactManager(null));
        }

        [Fact]
        public void ContactNullModelThrowsTest()
        {
            var manager = GetManager();

            Assert.Throws<ArgumentNullException>(() => manager.Contact(null));  
        }

        [Fact]
        public void CorrectSubjectTest()
        {
            var manager = GetManager();
            var model = new ContactModel
            {
                Name = Guid.NewGuid().ToString(),
                Email = Guid.NewGuid().ToString(),
                Body = Guid.NewGuid().ToString()
            };

            manager.Contact(model);

            var expectedSubject = $"Message from {model.Name}";

            _emailSenderMock.Verify(
                x => x.SendToSelf(It.Is<string>(y => Compare.EqualOrdinalIgnore(y, expectedSubject)), It.IsAny<string>()),
                Times.Once);
        }

        [Fact]
        public void CorrectBodyTest()
        {
            var manager = GetManager();
            var model = new ContactModel
            {
                Name = Guid.NewGuid().ToString(),
                Email = Guid.NewGuid().ToString(),
                Body = Guid.NewGuid().ToString()
            };

            manager.Contact(model);

            var expectedBody = $"Name: {model.Name}{Environment.NewLine}" +
                                $"Email: {model.Email}{Environment.NewLine}" +
                                $"Body:{Environment.NewLine}" +
                                model.Body;

            _emailSenderMock.Verify(
                x => x.SendToSelf(It.IsAny<string>(), It.Is<string>(y => Compare.EqualOrdinalIgnore(y, expectedBody))),
                Times.Once);
        }

        private ContactManager GetManager() =>
            new ContactManager(_emailSenderMock.Object);
    } 
}