namespace DetroitHarps.Repository.Test
{
    using System.Collections.Generic;
    using DetroitHarps.DataAccess.S3;
    using Moq;
    using Xunit;

    public class S3RepositoryBaseTest
    {
        private readonly Mock<IS3ObjectStore<TestEntity, int>> _mockObjectStore;
        private readonly ConcreteS3RepositoryBase _repository;

        public S3RepositoryBaseTest()
        {
            _mockObjectStore = new Mock<IS3ObjectStore<TestEntity, int>>();
            _repository = new ConcreteS3RepositoryBase(_mockObjectStore.Object);
        }

        [Fact]
        public void CreateCallsStorePutTest()
        {
            var entity = new TestEntity { Id = 1 };
            _repository.Create(entity);

            _mockObjectStore.Verify(x => x.Put(It.Is<TestEntity>(y => y == entity)));
        }

        [Fact]
        public void CreateReturnsIdTest()
        {
            var id = _repository.Create(new TestEntity { Id = 1 });

            Assert.Equal(1, id);
        }

        [Fact]
        public void UpdateCallsStorePutTest()
        {
            var entity = new TestEntity { Id = 1 };
            _repository.Update(entity);

            _mockObjectStore.Verify(x => x.Put(It.Is<TestEntity>(y => y == entity)));
        }

        [Fact]
        public void DeleteCallsStoreDeleteTest()
        {
            _repository.Delete(5);

            _mockObjectStore.Verify(x => x.Delete(It.Is<int>(y => y == 5)));
        }

        [Fact]
        public void ExistsReturnsTrueIfGetReturnsNonDefaultObjectTest()
        {
            _mockObjectStore.Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync(new TestEntity { Id = 1 });

            var result = _repository.Exists(5);

            Assert.True(result);
        }

        [Fact]
        public void ExistsReturnsFalseIfGetReturnsNonDefaultObjectTest()
        {
            _mockObjectStore.Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync(default(TestEntity));

            var result = _repository.Exists(5);

            Assert.False(result);
        }

        [Fact]
        public void GetSingleOrDefaultReturnsObjectFromStoreGetTest()
        {
            var id = 5;
            var entity = new TestEntity { Id = 1 };
            _mockObjectStore.Setup(x => x.Get(It.Is<int>(y => y == id))).ReturnsAsync(entity);

            var result = _repository.GetSingleOrDefault(5);

            Assert.Equal(entity, result);
        }

        [Fact]
        public void GetSingleOrDefaultReturnsDefaultFromStoreGetTest()
        {
            var id = 5;
            _mockObjectStore.Setup(x => x.Get(It.Is<int>(y => y == id))).ReturnsAsync(default(TestEntity));

            var result = _repository.GetSingleOrDefault(5);

            Assert.Equal(default(TestEntity), result);
        }

        [Fact]
        public void GetAllCallsStoreGetOnAllIdsReturnedFromStoreGetAllIdsTest()
        {
            var ids = new List<int> { 1, 2, 3 };

            _mockObjectStore.Setup(x => x.GetAllIds()).ReturnsAsync(ids);

            _repository.GetAll();

            _mockObjectStore.Verify(x => x.Get(It.Is<int>(y => y == 1)), Times.Once);
            _mockObjectStore.Verify(x => x.Get(It.Is<int>(y => y == 2)), Times.Once);
            _mockObjectStore.Verify(x => x.Get(It.Is<int>(y => y == 3)), Times.Once);
        }

        [Fact]
        public void GetAllCallsStoreReturnsAllObjectsReturnedFromStoreGetAllIdsTest()
        {
            var entity1 = new TestEntity { Id = 1 };
            var entity2 = new TestEntity { Id = 2 };
            var entity3 = new TestEntity { Id = 3 };
            var ids = new List<int> { entity1.Id, entity2.Id, entity3.Id };

            _mockObjectStore.Setup(x => x.GetAllIds()).ReturnsAsync(ids);
            _mockObjectStore.Setup(x => x.Get(It.Is<int>(y => y == 1))).ReturnsAsync(entity1);
            _mockObjectStore.Setup(x => x.Get(It.Is<int>(y => y == 2))).ReturnsAsync(entity2);
            _mockObjectStore.Setup(x => x.Get(It.Is<int>(y => y == 3))).ReturnsAsync(entity3);

            var result = _repository.GetAll();

            Assert.Equal(3, result.Count);
            Assert.Contains(entity1, result);
            Assert.Contains(entity2, result);
            Assert.Contains(entity3, result);
        }

        private class ConcreteS3RepositoryBase : S3RepositoryBase<TestEntity, int>
        {
            public ConcreteS3RepositoryBase(IS3ObjectStore<TestEntity, int> store)
                : base(store)
            {
            }
        }
    }
}