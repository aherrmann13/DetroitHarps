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
        private readonly Mock<IEventAccessor> _eventAccessor;
        private readonly Mock<ILogger<RegistrationManager>> _loggerMock;

        public RegistrationManagerTest()
        {
            _repositoryMock = new Mock<IRegistrationRepository>();
            _eventAccessor = new Mock<IEventAccessor>();
            _loggerMock = new Mock<ILogger<RegistrationManager>>();
        }

        [Fact]
        public void NullRepositoryInConstructorThrowsTestTest()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new RegistrationManager(
                    null,
                    _eventAccessor.Object,
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
                    _eventAccessor.Object,
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
        public void RegistrationCallsEventAccessorPerChildEventTest()
        {
            _eventAccessor
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
            _eventAccessor.Verify(
                x => x.GetSnapshot(It.Is<int>(y => y == 0)),
                Times.Once);
            _eventAccessor.Verify(
                x => x.GetSnapshot(It.Is<int>(y => y == 1)),
                Times.Once);
            _eventAccessor.Verify(
                x => x.GetSnapshot(It.Is<int>(y => y == 2)),
                Times.Once);
        }

        [Fact]
        public void RegistrationHasEventSnapshotOnRegisterTest()
        {
            _eventAccessor
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
        public void DeleteDoesNothingOnNonExistantIdTest()
        {
            var manager = GetManager();
            var id = 2;

            manager.Delete(id);

            _repositoryMock.Verify(
                x => x.GetSingleOrDefault(It.Is<int>(y => y.Equals(id))),
                Times.Once);
            _repositoryMock.Verify(
                x => x.Update(It.IsAny<Registration>()),
                Times.Never);
        }

        [Fact]
        public void DeleteSetsDisabledFlagAndUpdatesTest()
        {
            var registration = new Registration { IsDisabled = false };
            _repositoryMock
                .Setup(x => x.GetSingleOrDefault(It.IsAny<int>()))
                .Returns(registration);

            var manager = GetManager();
            var id = 2;

            manager.Delete(id);

            _repositoryMock.Verify(
                x => x.GetSingleOrDefault(It.Is<int>(y => y.Equals(id))),
                Times.Once);
            _repositoryMock.Verify(
                x => x.Update(It.Is<Registration>(y => y.Equals(registration) && y.IsDisabled)),
                Times.Once);
        }

        [Fact]
        public void DeleteChildDoesNothingOnNonExistantIdTest()
        {
            var manager = GetManager();
            var id = 2;

            manager.DeleteChild(id, string.Empty, string.Empty);

            _repositoryMock.Verify(
                x => x.GetSingleOrDefault(It.Is<int>(y => y.Equals(id))),
                Times.Once);
            _repositoryMock.Verify(
                x => x.Update(It.IsAny<Registration>()),
                Times.Never);
        }

        [Fact]
        public void DeleteChildDoesNothingOnNonExistantChildTest()
        {
            var registration = new Registration
            {
                IsDisabled = false,
                Children = new List<RegistrationChild>
                {
                    new RegistrationChild
                    {
                        FirstName = Guid.NewGuid().ToString(),
                        LastName = Guid.NewGuid().ToString(),
                        IsDisabled = false
                    }
                }
            };
            _repositoryMock
                .Setup(x => x.GetSingleOrDefault(It.IsAny<int>()))
                .Returns(registration);
            var manager = GetManager();
            var id = 2;

            manager.DeleteChild(id, Guid.NewGuid().ToString(), Guid.NewGuid().ToString());

            _repositoryMock.Verify(
                x => x.GetSingleOrDefault(It.Is<int>(y => y.Equals(id))),
                Times.Once);
            _repositoryMock.Verify(
                x => x.Update(It.IsAny<Registration>()),
                Times.Never);
        }

        [Fact]
        public void DeleteChildSetsDisabledFlagAndUpdatesTest()
        {
            var firstName = Guid.NewGuid().ToString();
            var lastName = Guid.NewGuid().ToString();
            var registration = new Registration
            {
                IsDisabled = false,
                Children = new List<RegistrationChild>
                {
                    new RegistrationChild
                    {
                        FirstName = firstName,
                        LastName = lastName,
                        IsDisabled = false
                    }
                }
            };

            _repositoryMock
                .Setup(x => x.GetSingleOrDefault(It.IsAny<int>()))
                .Returns(registration);
            var manager = GetManager();
            var id = 2;

            manager.DeleteChild(id, firstName, lastName);

            _repositoryMock.Verify(
                x => x.GetSingleOrDefault(It.Is<int>(y => y.Equals(id))),
                Times.Once);
            _repositoryMock.Verify(
                x => x.Update(
                    It.Is<Registration>(y =>
                        y.Equals(registration) &&
                        y.Children[0].Equals(registration.Children[0]) &&
                        y.Children.Count == registration.Children.Count &&
                        y.Children[0].IsDisabled)),
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
            _repositoryMock
                .Setup(x => x.GetMany(It.IsAny<Expression<Func<Registration, bool>>>()))
                .Returns(entities);

            var manager = GetManager();

            var modelsFromManager = manager.GetRegisteredParents(1234);

            _repositoryMock.Verify(
                x => x.GetMany(It.Is<Expression<Func<Registration, bool>>>(y => ValidateFiltersIsDisabled(y, 1234))),
                Times.Once());
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
            _repositoryMock.Setup(x => x.GetMany(It.IsAny<Expression<Func<Registration, bool>>>()))
                .Returns(entities);
            var manager = GetManager();

            var childrenFromManager = manager.GetRegisteredChildren(1234);

            _repositoryMock.Verify(
                x => x.GetMany(It.Is<Expression<Func<Registration, bool>>>(y => ValidateFiltersIsDisabled(y, 1234))),
                Times.Once());

            Assert.Equal(entities.SelectMany(x => x.Children).ToList().Count, childrenFromManager.Count());
            Assert.All(childrenFromManager, x => Assert.NotNull(x));
        }

        private bool ValidateFiltersIsDisabled(Expression<Func<Registration, bool>> expr, int year)
        {
            var shouldBeFalseDisabled = expr.Compile()(new Registration { IsDisabled = true, SeasonYear = year });
            var shouldBeTrue = expr.Compile()(new Registration { IsDisabled = false, SeasonYear = year });
            var shouldBeFalseYear = expr.Compile()(new Registration { IsDisabled = true, SeasonYear = year + 1 });

            return shouldBeTrue && !shouldBeFalseDisabled && !shouldBeFalseYear;
        }

        private RegistrationManager GetManager() =>
            new RegistrationManager(
                _repositoryMock.Object,
                _eventAccessor.Object,
                _loggerMock.Object);
    }
}