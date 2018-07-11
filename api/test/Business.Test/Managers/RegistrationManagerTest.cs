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

    [Collection("AutoMapper")]
    public class RegistrationManagerTest
    {
        private readonly Mock<IRegistrationRepository> _registrationRepositoryMock;
        
        public RegistrationManagerTest()
        {
            _registrationRepositoryMock = new Mock<IRegistrationRepository>();
        }

        [Fact]
        public void NullRepositoryInConstructorThrowsTestTest()
        {
            Assert.Throws<ArgumentNullException>(() => new RegistrationManager(null));
        }

        [Fact]
        public void RegisterNullModelThrowsTest()
        {
            var manager = GetManager();

            Assert.Throws<ArgumentNullException>(() => manager.Register(null));  
        }

        [Fact]
        public void RegisterModelPassedToRepositoryTest()
        {
            var manager = GetManager();
            var model = new RegisterModel();

            manager.Register(model);

            _registrationRepositoryMock.Verify(
                x => x.Create(It.Is<Registration>(y => y != null)),
                Times.Once);
        }

        [Fact]
        public void AddChildNullModelThrowsTest()
        {
            var manager = GetManager();

            Assert.Throws<ArgumentNullException>(() => manager.AddChild(null));  
        }

        [Fact]
        public void AddChildNoParentThrowsTest()
        {
            var manager = GetManager();
            var model = new RegisterAddChildModel();

            Assert.Throws<InvalidOperationException>(() => manager.AddChild(model));  
        }

        [Fact]
        public void AddChildPassesCorrectEntityToUpdateTest()
        {
            var entity = new Registration();
            _registrationRepositoryMock.Setup(x => x.GetSingleOrDefault(It.IsAny<Expression<Func<Registration, bool>>>()))
                .Returns(entity);
            var manager = GetManager();
            var model = new RegisterAddChildModel();

            manager.AddChild(model);

            _registrationRepositoryMock.Verify(x => x.Update(It.Is<Registration>(y => y.Equals(entity))), Times.Once);
        }

        [Fact]
        public void AddChildPassesAddsToEntityChildListTest()
        {
            var entity = new Registration();
            var currentChildren = entity.Children.Count;
            _registrationRepositoryMock.Setup(x => x.GetSingleOrDefault(It.IsAny<Expression<Func<Registration, bool>>>()))
                .Returns(entity);
            var manager = GetManager();
            var model = new RegisterAddChildModel();

            manager.AddChild(model);

            _registrationRepositoryMock.Verify(
                x => x.Update(
                    It.Is<Registration>(y => y.Children.Count.Equals(currentChildren + 1))),
                Times.Once);
        }

        [Fact]
        public void GetAllChildrenReturnsMappedModelsTest()
        {
            var entities = new List<Registration>
            {
                new Registration
                {
                    Children = new List<RegistrationChild>
                    {
                        new RegistrationChild(),
                        new RegistrationChild()
                    }
                },
                new Registration
                {
                    Children = new List<RegistrationChild>
                    {
                        new RegistrationChild(),
                        new RegistrationChild()
                    }
                }
            };
            _registrationRepositoryMock.Setup(x => x.GetAll())
                .Returns(entities);
            var manager = GetManager();

            var childrenFromManager = manager.GetAllRegisteredChildren();

            Assert.Equal(entities.SelectMany(x => x.Children).ToList().Count, childrenFromManager.Count());

            Assert.All(childrenFromManager, x => Assert.NotNull(x));
        }

        [Fact]
        public void GetAllChildrenReturnsCorrectEmailAddressesTest()
        {
            var entities = new List<Registration>
            {
                new Registration
                {
                    ContactInformation = new RegistrationContactInformation
                    {
                        Email = Guid.NewGuid().ToString()
                    },
                    Children = new List<RegistrationChild>
                    {
                        new RegistrationChild(),
                        new RegistrationChild()
                    }
                },
                new Registration
                {
                    ContactInformation = new RegistrationContactInformation
                    {
                        Email = Guid.NewGuid().ToString()
                    },
                    Children = new List<RegistrationChild>
                    {
                        new RegistrationChild(),
                        new RegistrationChild()
                    }
                }
            };
            _registrationRepositoryMock.Setup(x => x.GetAll())
                .Returns(entities);
            var manager = GetManager();

            var childrenFromManager = manager.GetAllRegisteredChildren().ToList();

            Assert.Equal(entities[0].ContactInformation.Email, childrenFromManager[0].EmailAddress);
            Assert.Equal(entities[0].ContactInformation.Email, childrenFromManager[1].EmailAddress);
            Assert.Equal(entities[1].ContactInformation.Email, childrenFromManager[2].EmailAddress);
            Assert.Equal(entities[1].ContactInformation.Email, childrenFromManager[3].EmailAddress);
        }

        [Fact]
        public void NullRepositoryInConstructorThrowsTest()
        {
            Assert.Throws<ArgumentNullException>(() => new RegistrationManager(null));
        }

        private RegistrationManager GetManager() =>
            new RegistrationManager(_registrationRepositoryMock.Object);
    } 
}