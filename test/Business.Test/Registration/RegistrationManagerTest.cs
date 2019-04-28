namespace DetroitHarps.Business.Test.Registration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using DetroitHarps.Business.Common.Exceptions;
    using DetroitHarps.Business.Registration;
    using DetroitHarps.Business.Registration.Entities;
    using DetroitHarps.Business.Registration.Models;
    using Microsoft.Extensions.Logging;
    using Moq;
    using Xunit;

    [Collection("AutoMapper")]
    public class RegistrationManagerTest
    {
        private readonly Mock<IRegistrationRepository> _repositoryMock;
        private readonly Mock<ILogger<RegistrationManager>> _loggerMock;

        public RegistrationManagerTest()
        {
            _repositoryMock = new Mock<IRegistrationRepository>();
            _loggerMock = new Mock<ILogger<RegistrationManager>>();
        }

        [Fact]
        public void NullRepositoryInConstructorThrowsTestTest()
        {
            Assert.Throws<ArgumentNullException>(() => new RegistrationManager(null, _loggerMock.Object));
        }

        [Fact]
        public void NullLoggerInConstructorThrowsTestTest()
        {
            Assert.Throws<ArgumentNullException>(() => new RegistrationManager(_repositoryMock.Object, null));
        }

        [Fact]
        public void RegisterNullModelThrowsTest()
        {
            var manager = GetManager();

            Assert.Throws<BusinessException>(() => manager.Register(null));
        }

        [Fact]
        public void RegisterModelPassedToRepositoryTest()
        {
            var manager = GetManager();
            var model = new RegisterModel();

            manager.Register(model);

            _repositoryMock.Verify(
                x => x.Create(It.Is<Registration>(y => y != null)),
                Times.Once);
        }

        [Fact]
        public void DeleteIdPassedToRepositoryTest()
        {
            var manager = GetManager();
            var id = 2;

            manager.Delete(id);

            _repositoryMock.Verify(
                x => x.Delete(It.Is<int>(y => y.Equals(id))),
                Times.Once);
        }

        [Fact]
        public void GetAllParentsReturnsModelsTest()
        {
            var entities = new List<Registration>
            {
                new Registration
                {
                    Parent = new RegistrationParent(),
                    ContactInformation = new RegistrationContactInformation(),
                    Children = new List<RegistrationChild>
                    {
                        new RegistrationChild(),
                        new RegistrationChild()
                    }
                },
                new Registration
                {
                    Parent = new RegistrationParent(),
                    ContactInformation = new RegistrationContactInformation(),
                    Children = new List<RegistrationChild>
                    {
                        new RegistrationChild(),
                        new RegistrationChild()
                    }
                }
            };
            _repositoryMock.Setup(x => x.GetAll())
                .Returns(entities);

            var manager = GetManager();

            var modelsFromManager = manager.GetAllRegisteredParents();

            Assert.Equal(entities.Count, modelsFromManager.Count());
            Assert.All(modelsFromManager, x => Assert.NotNull(x));
        }

        [Fact]
        public void GetAllChildrenReturnsMappedModelsTest()
        {
            var entities = new List<Registration>
            {
                new Registration
                {
                    Parent = new RegistrationParent(),
                    ContactInformation = new RegistrationContactInformation(),
                    Children = new List<RegistrationChild>
                    {
                        new RegistrationChild(),
                        new RegistrationChild()
                    }
                },
                new Registration
                {
                    Parent = new RegistrationParent(),
                    ContactInformation = new RegistrationContactInformation(),
                    Children = new List<RegistrationChild>
                    {
                        new RegistrationChild(),
                        new RegistrationChild()
                    }
                }
            };
            _repositoryMock.Setup(x => x.GetAll())
                .Returns(entities);
            var manager = GetManager();

            var childrenFromManager = manager.GetAllRegisteredChildren();

            Assert.Equal(entities.SelectMany(x => x.Children).ToList().Count, childrenFromManager.Count());

            Assert.All(childrenFromManager, x => Assert.NotNull(x));
        }

        private RegistrationManager GetManager() =>
            new RegistrationManager(_repositoryMock.Object, _loggerMock.Object);
    }
}