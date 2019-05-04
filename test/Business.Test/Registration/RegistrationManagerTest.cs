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
        private readonly Mock<IEventSnapshotProvider> _eventSnapshotProvider;
        private readonly Mock<ILogger<RegistrationManager>> _loggerMock;

        public RegistrationManagerTest()
        {
            _repositoryMock = new Mock<IRegistrationRepository>();
            _eventSnapshotProvider = new Mock<IEventSnapshotProvider>();
            _loggerMock = new Mock<ILogger<RegistrationManager>>();
        }

        [Fact]
        public void NullRepositoryInConstructorThrowsTestTest()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new RegistrationManager(
                    null,
                    _eventSnapshotProvider.Object,
                    _loggerMock.Object));
        }

        [Fact]
        public void NullSnapshotInConstructorThrowsTestTest()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new RegistrationManager(
                    _repositoryMock.Object,
                    null,
                    _loggerMock.Object));
        }

        [Fact]
        public void NullLoggerInConstructorThrowsTestTest()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new RegistrationManager(
                    _repositoryMock.Object,
                    _eventSnapshotProvider.Object,
                    null));
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
        public void RegisterationEntityHasCorrectPaymentAmountSingleChildTest()
        {
            var manager = GetManager();
            var model = new RegisterModel
            {
                Children = new List<RegisterChildModel>
                {
                    new RegisterChildModel()
                }
            };

            manager.Register(model);

            _repositoryMock.Verify(
                x => x.Create(It.Is<Registration>(y => y.PaymentInformation.Amount == 20)),
                Times.Once);
        }

        [Fact]
        public void RegisterationEntityHasCorrectPaymentAmountMultipleChildrenTest()
        {
            var manager = GetManager();
            var model = new RegisterModel
            {
                Children = new List<RegisterChildModel>
                {
                    new RegisterChildModel(),
                    new RegisterChildModel()
                }
            };

            manager.Register(model);

            _repositoryMock.Verify(
                x => x.Create(It.Is<Registration>(y => y.PaymentInformation.Amount == 30)),
                Times.Once);
        }

        [Fact]
        public void RegistrationCallsEventSnapshotProviderPerChildEventTest()
        {
            _eventSnapshotProvider
                .Setup(x => x.GetSnapshot(It.IsAny<int>()))
                .Returns(new RegistrationChildEventSnapshot());
            var manager = GetManager();
            var counter = 0;
            var model = new RegisterModel
            {
                Children = new List<RegisterChildModel>
                {
                    new RegisterChildModel
                    {
                        Events = new List<RegisterChildEventModel>
                        {
                            new RegisterChildEventModel { EventId = counter++ },
                            new RegisterChildEventModel { EventId = counter++ }
                        }
                    },
                    new RegisterChildModel
                    {
                        Events = new List<RegisterChildEventModel>
                        {
                            new RegisterChildEventModel { EventId = counter }
                        }
                    }
                }
            };

            manager.Register(model);
            _eventSnapshotProvider.Verify(
                x => x.GetSnapshot(It.Is<int>(y => y == 0)),
                Times.Once);
            _eventSnapshotProvider.Verify(
                x => x.GetSnapshot(It.Is<int>(y => y == 1)),
                Times.Once);
            _eventSnapshotProvider.Verify(
                x => x.GetSnapshot(It.Is<int>(y => y == 2)),
                Times.Once);
        }

        [Fact]
        public void RegistrationHasEventSnapshotOnRegisterTest()
        {
            _eventSnapshotProvider
                .Setup(x => x.GetSnapshot(It.IsAny<int>()))
                .Returns(new RegistrationChildEventSnapshot());
            var manager = GetManager();
            var counter = 0;
            var model = new RegisterModel
            {
                Children = new List<RegisterChildModel>
                {
                    new RegisterChildModel
                    {
                        Events = new List<RegisterChildEventModel>
                        {
                            new RegisterChildEventModel { EventId = counter++ },
                            new RegisterChildEventModel { EventId = counter++ }
                        }
                    },
                    new RegisterChildModel
                    {
                        Events = new List<RegisterChildEventModel>
                        {
                            new RegisterChildEventModel { EventId = counter }
                        }
                    }
                }
            };

            manager.Register(model);
            _repositoryMock.Verify(
                x => x.Create(It.Is<Registration>(y =>
                    y.Children
                        .SelectMany(z => z.Events)
                        .SingleOrDefault(z =>
                            z.EventId == 0 && z.EventSnapshot != null) != null)),
                Times.Once);
            _repositoryMock.Verify(
                x => x.Create(It.Is<Registration>(y =>
                    y.Children
                        .SelectMany(z => z.Events)
                        .SingleOrDefault(z =>
                            z.EventId == 1 && z.EventSnapshot != null) != null)),
                Times.Once);
            _repositoryMock.Verify(
                x => x.Create(It.Is<Registration>(y =>
                    y.Children
                        .SelectMany(z => z.Events)
                        .SingleOrDefault(z =>
                            z.EventId == 2 && z.EventSnapshot != null) != null)),
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
            new RegistrationManager(
                _repositoryMock.Object,
                _eventSnapshotProvider.Object,
                _loggerMock.Object);
    }
}