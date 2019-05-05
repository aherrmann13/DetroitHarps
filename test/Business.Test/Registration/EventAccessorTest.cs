namespace DetroitHarps.Business.Test.Registration
{
    using System;
    using DetroitHarps.Business.Common.Exceptions;
    using DetroitHarps.Business.Registration;
    using DetroitHarps.Business.Schedule;
    using DetroitHarps.Business.Schedule.Entities;
    using Moq;
    using Xunit;

    [Collection("AutoMapper")]
    public class EventAccessorTest
    {
        private readonly Mock<IEventRepository> _repositoryMock;
        private readonly EventAccessor _eventAccessor;

        public EventAccessorTest()
        {
            _repositoryMock = new Mock<IEventRepository>();
            _eventAccessor = new EventAccessor(_repositoryMock.Object);
        }

        [Fact]
        public void NullRepositoryInConstructorThrowsTest()
        {
            Assert.Throws<ArgumentNullException>(() => new EventAccessor(null));
        }

        [Fact]
        public void GetSnapshotEventWithIdDoesNotExistThrowsTest()
        {
            _repositoryMock
                .Setup(x => x.GetSingleOrDefault(It.IsAny<int>()))
                .Returns((Event)null);

            var exception = Assert.Throws<BusinessException>(() =>
                _eventAccessor.GetSnapshot(1));
            _repositoryMock.Verify(x => x.GetSingleOrDefault(It.Is<int>(y => y == 1)), Times.Once);
            Assert.Equal("Event with id 1 doesn't exist", exception.Message);
        }

        [Fact]
        public void GetSnapshotReturnsSnapshotSuccessfullyTest()
        {
            _repositoryMock
                .Setup(x => x.GetSingleOrDefault(It.IsAny<int>()))
                .Returns(new Event());

            var snapshot = _eventAccessor.GetSnapshot(1);

            _repositoryMock.Verify(x => x.GetSingleOrDefault(It.Is<int>(y => y == 1)), Times.Once);
            Assert.NotNull(snapshot);
        }

        [Fact]
        public void GetSnapshotCachesSnapshotSuccessfullyTest()
        {
            _repositoryMock
                .Setup(x => x.GetSingleOrDefault(It.IsAny<int>()))
                .Returns(new Event());

            _eventAccessor.GetSnapshot(1);
            _eventAccessor.GetSnapshot(1);

            _repositoryMock.Verify(
                x => x.GetSingleOrDefault(It.Is<int>(y => y == 1)),
                Times.Once);
        }

        [Fact]
        public void GetNameEventWithIdDoesNotExistThrowsTest()
        {
            _repositoryMock
                .Setup(x => x.GetSingleOrDefault(It.IsAny<int>()))
                .Returns((Event)null);

            var exception = Assert.Throws<BusinessException>(() =>
                _eventAccessor.GetName(1));
            _repositoryMock.Verify(x => x.GetSingleOrDefault(It.Is<int>(y => y == 1)), Times.Once);
            Assert.Equal("Event with id 1 doesn't exist", exception.Message);
        }

        [Fact]
        public void GetNameReturnsNameSuccessfullyTest()
        {
            _repositoryMock
                .Setup(x => x.GetSingleOrDefault(It.IsAny<int>()))
                .Returns(new Event { Title = "test" });

            var name = _eventAccessor.GetName(1);

            _repositoryMock.Verify(x => x.GetSingleOrDefault(It.Is<int>(y => y == 1)), Times.Once);
            Assert.Equal("test", name);
        }

        [Fact]
        public void CachesSnapshotSuccessfullyTest()
        {
            _repositoryMock
                .Setup(x => x.GetSingleOrDefault(It.IsAny<int>()))
                .Returns(new Event { Title = "test" });

            _eventAccessor.GetName(1);
            _eventAccessor.GetName(1);

            _repositoryMock.Verify(x => x.GetSingleOrDefault(It.Is<int>(y => y == 1)), Times.Once);
        }
    }
}