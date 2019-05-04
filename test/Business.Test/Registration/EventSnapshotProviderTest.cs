namespace DetroitHarps.Business.Test.Registration
{
    using System;
    using DetroitHarps.Business.Common.Exceptions;
    using DetroitHarps.Business.Registration;
    using DetroitHarps.Business.Schedule;
    using DetroitHarps.Business.Schedule.Entities;
    using Moq;
    using Xunit;

    public class EventSnapshotProviderTest
    {
        private readonly Mock<IEventRepository> _repositoryMock;
        private readonly EventSnapshotProvider _eventSnapshotProvider;

        public EventSnapshotProviderTest()
        {
            _repositoryMock = new Mock<IEventRepository>();
            _eventSnapshotProvider = new EventSnapshotProvider(_repositoryMock.Object);
        }

        [Fact]
        public void NullRepositoryInConstructorThrowsTest()
        {
            Assert.Throws<ArgumentNullException>(() => new EventSnapshotProvider(null));
        }

        [Fact]
        public void EventWithIdDoesNotExistThrowsTest()
        {
            _repositoryMock
                .Setup(x => x.GetSingleOrDefault(It.IsAny<int>()))
                .Returns((Event)null);

            var exception = Assert.Throws<BusinessException>(() =>
                _eventSnapshotProvider.GetSnapshot(1));
            _repositoryMock.Verify(x => x.GetSingleOrDefault(It.Is<int>(y => y == 1)), Times.Once);
            Assert.Equal("Event with id 1 doesn't exist", exception.Message);
        }

        [Fact]
        public void ReturnsSnapshotSuccessfullyTest()
        {
            _repositoryMock
                .Setup(x => x.GetSingleOrDefault(It.IsAny<int>()))
                .Returns(new Event());

            var snapshot = _eventSnapshotProvider.GetSnapshot(1);

            _repositoryMock.Verify(x => x.GetSingleOrDefault(It.Is<int>(y => y == 1)), Times.Once);
            Assert.NotNull(snapshot);
        }
    }
}