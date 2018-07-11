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
    public class ScheduleManagerTest
    {
        private readonly Mock<IEventRepository> _eventRepositoryMock;
        
        public ScheduleManagerTest()
        {
            _eventRepositoryMock = new Mock<IEventRepository>();
        }

        [Fact]
        public void NullRepositoryInConstructorThrowsTestTest()
        {
            Assert.Throws<ArgumentNullException>(() => new ScheduleManager(null));
        }

        [Fact]
        public void CreateNullModelThrowsTest()
        {
            var manager = GetManager();

            Assert.Throws<ArgumentNullException>(() => manager.Create(null));  
        }

        [Fact]
        public void CreateModelPassedToRepositoryTest()
        {
            var manager = GetManager();
            var model = new EventCreateModel();

            manager.Create(model);

            _eventRepositoryMock.Verify(
                x => x.Create(It.Is<Event>(y => y != null)),
                Times.Once);
        }

        [Fact]
        public void CreateReturnsIdTest()
        {
            var id = 5;
            _eventRepositoryMock.Setup(x => x.Create(It.IsAny<Event>()))
                .Returns(id);
            var manager = GetManager();
            var model = new EventCreateModel();

            var idFromManager = manager.Create(model);

            Assert.Equal(id, idFromManager);
        }

        [Fact]
        public void UpdateNullModelThrowsTest()
        {
            var manager = GetManager();

            Assert.Throws<ArgumentNullException>(() => manager.Update(null));  
        }

        [Fact]
        public void UpdateModelPassedToRepositoryTest()
        {
            var manager = GetManager();
            var model = new EventModel();

            manager.Update(model);

            _eventRepositoryMock.Verify(
                x => x.Update(It.Is<Event>(y => y != null)),
                Times.Once);
        }

        [Fact]
        public void DeleteIdPassedToRepositoryTest()
        {
            var manager = GetManager();
            var id = 2;

            manager.Delete(id);

            _eventRepositoryMock.Verify(
                x => x.Delete(It.Is<int>(y => y.Equals(id))),
                Times.Once);
        }

        [Fact]
        public void GetAllReturnsModelsTest()
        {
            var models = new List<Event>()
            {
                new Event(),
                new Event()
            };
            _eventRepositoryMock.Setup(x => x.GetAll())
                .Returns(models);

            var manager = GetManager();

            var modelsFromManager = manager.GetAll();

            Assert.Equal(models.Count, modelsFromManager.Count());
            Assert.All(modelsFromManager, x => Assert.NotNull(x));
        }

        [Fact]
        public void CurrentDatePassedIntoFilterExpressionTest()
        {
            var manager = GetManager();
            var expectedExpression = 

            manager.GetUpcoming();

            _eventRepositoryMock.Verify(
                x => x.GetMany(It.Is<Expression<Func<Event, bool>>>(y => VerifyExpression(y))),
                Times.Once);
        }

        [Fact]
        public void GetUpcomingDoesNotFilterOnNullEndDateTest()
        {
            var models = new List<Event>()
            {
                new Event(),
                new Event()
            };
            _eventRepositoryMock.Setup(x => x.GetMany(It.IsAny<Expression<Func<Event, bool>>>()))
                .Returns(models);

            var manager = GetManager();

            var modelsFromManager = manager.GetUpcoming();

            Assert.Equal(models.Count, modelsFromManager.Count());
            Assert.All(modelsFromManager, x => Assert.NotNull(x));
        }

        [Fact]
        public void GetUpcomingFiltersOnEndDateTest()
        {
            var date = DateTime.Now;
            var models = new List<Event>()
            {
                new Event
                {
                    Date = date.AddDays(1)
                },
                new Event
                {
                    Date = date
                },
                new Event
                {
                    Date = date.AddDays(-1)
                },
            };
            _eventRepositoryMock.Setup(x => x.GetMany(It.IsAny<Expression<Func<Event, bool>>>()))
                .Returns(models);

            var manager = GetManager();

            var modelsFromManager = manager.GetUpcoming(date);

            Assert.Equal(models.Count - 1, modelsFromManager.Count());
            Assert.All(modelsFromManager, x => Assert.NotNull(x));
        }

        [Fact]
        public void NullRepositoryInConstructorThrowsTest()
        {
            Assert.Throws<ArgumentNullException>(() => new ScheduleManager(null));
        }

        private bool VerifyExpression(Expression<Func<Event, bool>> expr)
        {
            // TODO : this feels like a hack
            var func = expr.Compile();
            Assert.True(func.Invoke(new Event { Date = DateTime.Now.ToUniversalTime().Date}));
            Assert.False(func.Invoke(new Event { Date = DateTime.Now.ToUniversalTime().Date.AddDays(-1)}));

            return true;
        }

        private ScheduleManager GetManager() =>
            new ScheduleManager(_eventRepositoryMock.Object);
    } 
}