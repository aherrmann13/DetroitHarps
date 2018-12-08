namespace DetroitHarps.Repository.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DetroitHarps.DataAccess;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using Xunit;

    public class RepositoryBaseTest
    {
        private readonly Mock<DetroitHarpsDbContext> _dbContextMock;
        private readonly ConcreteRepository _repository;

        public RepositoryBaseTest()
        {
            _dbContextMock = new Mock<DetroitHarpsDbContext>(new DbContextOptions<DetroitHarpsDbContext>());
            _repository = new ConcreteRepository(_dbContextMock.Object);
        }

        [Fact]
        public void CreateThrowsOnNullObjectTest()
        {
            Assert.Throws<ArgumentNullException>(() => _repository.Create(null));
        }

        [Fact]
        public void CreateAddsToDbContextTest()
        {
            var entity = new TestEntity();

            _repository.Create(entity);

            _dbContextMock.Verify(x => x.Add(It.Is<TestEntity>(y => y.Equals(entity))), Times.Once);
            _dbContextMock.Verify(x => x.SaveChanges(), Times.Once);
        }

        [Fact]
        public void CreateReturnsIdTest()
        {
            var entity = new TestEntity { Id = 2 };

            // TODO: maybe use Moq to have value change?
            var returnedId = _repository.Create(entity);

            Assert.Equal(entity.Id, returnedId);
        }

        [Fact]
        public void UpdateThrowsOnNullTest()
        {
            Assert.Throws<ArgumentNullException>(() => _repository.Update(null));
        }

        [Fact]
        public void UpdateSavesChangesTest()
        {
            var entity = new TestEntity();

            _repository.Update(entity);

            _dbContextMock.Verify(x => x.SaveChanges(), Times.Once);
        }

        [Fact]
        public void DeleteIgnoresIfNotExistTest()
        {
            _dbContextMock
                .Setup(x => x.Set<TestEntity>())
                .Returns(new List<TestEntity>().AsQueryableMockDbSet());

            _repository.Delete(2);

            _dbContextMock.Verify(x => x.SaveChanges(), Times.Never);
        }

        [Fact]
        public void DeleteRemovesWhenExists()
        {
            var entities = new List<TestEntity> { new TestEntity { Id = 2 } };
            _dbContextMock
                .Setup(x => x.Set<TestEntity>())
                .Returns(entities.AsQueryableMockDbSet());

            _repository.Delete(entities[0].Id);

            _dbContextMock.Verify(x => x.Remove(It.Is<TestEntity>(y => y.Equals(entities[0]))));
            _dbContextMock.Verify(x => x.SaveChanges(), Times.Once);
        }

        [Fact]
        public void ExistsReturnsValueFromDbContextTest()
        {
            var entities = new List<TestEntity> { new TestEntity { Id = 5 } };
            _dbContextMock.Setup(x => x.Set<TestEntity>()).Returns(entities.AsQueryableMockDbSet());

            var expectedTrueValue = _repository.Exists(entities[0].Id);
            var expectedFalseValue = _repository.Exists(entities[0].Id + 1);

            Assert.True(expectedTrueValue);
            Assert.False(expectedFalseValue);
        }

        [Fact]
        public void GetAllReturnsAllTest()
        {
            var entities = new List<TestEntity> { new TestEntity(), new TestEntity() };
            _dbContextMock.Setup(x => x.Set<TestEntity>()).Returns(entities.AsQueryableMockDbSet());

            var entitiesReturned = _repository.GetAll();

            Assert.Equal(entitiesReturned, entities);
        }

        [Fact]
        public void GetManyThrowsOnNullTest()
        {
            Assert.Throws<ArgumentNullException>(() => _repository.GetMany(null));
        }

        [Fact]
        public void GetManyFiltersCorrectlyTest()
        {
            var entities = new List<TestEntity> { new TestEntity { Id = 2 }, new TestEntity { Id = 3 } };
            _dbContextMock.Setup(x => x.Set<TestEntity>()).Returns(entities.AsQueryableMockDbSet());

            var entitiesReturned = _repository.GetMany(x => x.Id == entities[0].Id);

            Assert.Single(entitiesReturned);
            Assert.Equal(entities[0], entitiesReturned[0]);
        }

        [Fact]
        public void GetSingleOrDefaultThrowsOnNullTest()
        {
            Assert.Throws<ArgumentNullException>(() => _repository.GetSingleOrDefault(null));
        }

        [Fact]
        public void GetSingleOrDefaultThrowsOnMultipleTest()
        {
            var entities = new List<TestEntity> { new TestEntity { Id = 2 }, new TestEntity { Id = 3 } };
            _dbContextMock.Setup(x => x.Set<TestEntity>()).Returns(entities.AsQueryableMockDbSet());

            Assert.Throws<InvalidOperationException>(() => _repository.GetSingleOrDefault(x => true));
        }

        [Fact]
        public void GetSingleOrDefaultFiltersCorrectlyTest()
        {
            var entities = new List<TestEntity> { new TestEntity { Id = 2 }, new TestEntity { Id = 3 } };
            _dbContextMock.Setup(x => x.Set<TestEntity>()).Returns(entities.AsQueryableMockDbSet());

            var entityReturned = _repository.GetSingleOrDefault(x => x.Id == entities[0].Id);

            Assert.Equal(entities[0], entityReturned);
        }

        [Fact]
        public void GetSingleOrDefaultReturnsNullOnNoneTest()
        {
            var entities = new List<TestEntity> { new TestEntity { Id = 2 }, new TestEntity { Id = 3 } };
            _dbContextMock.Setup(x => x.Set<TestEntity>()).Returns(entities.AsQueryableMockDbSet());

            var entityReturned = _repository.GetSingleOrDefault(x => false);

            Assert.Null(entityReturned);
        }

        [Fact]
        public void GetSingleOrDefaultByIdThrowsOnMultipleTest()
        {
            var commonId = 2;
            var entities = new List<TestEntity> { new TestEntity { Id = commonId }, new TestEntity { Id = commonId } };
            _dbContextMock.Setup(x => x.Set<TestEntity>()).Returns(entities.AsQueryableMockDbSet());

            Assert.Throws<InvalidOperationException>(() => _repository.GetSingleOrDefault(x => x.Id == commonId));
        }

        [Fact]
        public void GetSingleOrDefaultByidFiltersCorrectlyTest()
        {
            var entities = new List<TestEntity> { new TestEntity { Id = 2 }, new TestEntity { Id = 3 } };
            _dbContextMock.Setup(x => x.Set<TestEntity>()).Returns(entities.AsQueryableMockDbSet());

            var entityReturned = _repository.GetSingleOrDefault(entities[0].Id);

            Assert.Equal(entities[0], entityReturned);
        }

        [Fact]
        public void GetSingleOrDefaultByIdReturnsNullOnNoneTest()
        {
            var entities = new List<TestEntity> { new TestEntity { Id = 2 }, new TestEntity { Id = 3 } };
            _dbContextMock.Setup(x => x.Set<TestEntity>()).Returns(entities.AsQueryableMockDbSet());

            var entityReturned = _repository.GetSingleOrDefault(entities.Max(x => x.Id) + 1);

            Assert.Null(entityReturned);
        }
    }
}