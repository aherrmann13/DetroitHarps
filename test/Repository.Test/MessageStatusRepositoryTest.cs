namespace DetroitHarps.Repository.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DetroitHarps.DataAccess.S3;
    using DetroitHarps.Repository.Internal;
    using Moq;
    using Xunit;

    public class MessageStatusRepositoryTest
    {
        private readonly Mock<IS3ObjectStore<MessageStatusContainer, string>> _mockMessageStatusStore;
        private readonly MessageStatusRepository _repository;
        private readonly MessageStatusContainer _fakeMessageStatusContainer;

        public MessageStatusRepositoryTest()
        {
            _mockMessageStatusStore = new Mock<IS3ObjectStore<MessageStatusContainer, string>>();
            _repository = new MessageStatusRepository(_mockMessageStatusStore.Object);
            _fakeMessageStatusContainer = new MessageStatusContainer
            {
                Id = "MessageStatusContainer",
                UnreadMessages = new HashSet<Guid>()
                {
                    Guid.NewGuid(),
                    Guid.NewGuid()
                }
            };
            _mockMessageStatusStore.Setup(x => x.Get(It.Is<string>(y => y == "MessageStatusContainer")))
                .ReturnsAsync(_fakeMessageStatusContainer);
        }

        [Fact]
        public void GetUnreadMessageIdsLoadsFromStoreOnceTest()
        {
            // warm up the cache
            _repository.GetUnreadMessageIds();
            _mockMessageStatusStore.Verify(x => x.Get(It.Is<string>(y => y == "MessageStatusContainer")), Times.Once);

            var unreadIdsCall1 = _repository.GetUnreadMessageIds();
            var unreadIdsCall2 = _repository.GetUnreadMessageIds();

            Assert.Equal(_fakeMessageStatusContainer.UnreadMessages, unreadIdsCall1);
            Assert.Equal(_fakeMessageStatusContainer.UnreadMessages, unreadIdsCall2);
            _mockMessageStatusStore.VerifyNoOtherCalls();
        }

        [Fact]
        public void GetUnreadMessageIdsReturnsIdsAfterIdsAddedTest()
        {
            _repository.GetUnreadMessageIds();

            var newId1 = Guid.NewGuid();
            var newId2 = Guid.NewGuid();
            _repository.SetAsUnread(newId1);
            _repository.SetAsUnread(newId2);

            var unreadIds = _repository.GetUnreadMessageIds();

            var expectedList = new List<Guid>(_fakeMessageStatusContainer.UnreadMessages);
            expectedList.Add(newId1);
            expectedList.Add(newId2);

            Assert.Equal(expectedList, unreadIds);

            _mockMessageStatusStore.Verify(x => x.Get(It.Is<string>(y => y == "MessageStatusContainer")), Times.Once);
        }

        [Fact]
        public void GetUnreadMessageIdsReturnsIdsAfterIdsRemovedTest()
        {
            _repository.GetUnreadMessageIds();

            var savedUnreadIds = _fakeMessageStatusContainer.UnreadMessages.ToList();
            _repository.SetAsRead(savedUnreadIds[0]);

            var unreadIds = _repository.GetUnreadMessageIds();

            var expectedList = new List<Guid> { savedUnreadIds[1] };

            Assert.Equal(expectedList, unreadIds);

            _mockMessageStatusStore.Verify(x => x.Get(It.Is<string>(y => y == "MessageStatusContainer")), Times.Once);
        }

        [Fact]
        public void GetUnreadMessageIdsReturnsEmptyObjectObjectDoesntExistTest()
        {
            _mockMessageStatusStore.Setup(x => x.Get(It.Is<string>(y => y == "MessageStatusContainer")))
                .ReturnsAsync((MessageStatusContainer)null);

            var unreadIds = _repository.GetUnreadMessageIds();
            Assert.Empty(unreadIds);
        }

        [Fact]
        public void SetAsReadLoadsIdsFromStoreIfCacheNotInitializedTest()
        {
            var savedUnreadIds = _fakeMessageStatusContainer.UnreadMessages.ToList();

            _repository.SetAsRead(savedUnreadIds[0]);
            _mockMessageStatusStore.Verify(x => x.Get(It.Is<string>(y => y == "MessageStatusContainer")), Times.Once);

            var unreadIds = _repository.GetUnreadMessageIds();
            var expectedList = new List<Guid> { savedUnreadIds[1] };

            Assert.Equal(expectedList, unreadIds);
        }

        [Fact]
        public void SetAsReadDoesNotLoadIdsFromStoreIfCacheInitializedTest()
        {
            // warm up cache and verify call
            _repository.GetUnreadMessageIds();
            _mockMessageStatusStore.Verify(x => x.Get(It.Is<string>(y => y == "MessageStatusContainer")), Times.Once);

            var savedUnreadIds = _fakeMessageStatusContainer.UnreadMessages.ToList();

            _repository.SetAsRead(savedUnreadIds[0]);

            // still one invocation
            _mockMessageStatusStore.Verify(x => x.Get(It.Is<string>(y => y == "MessageStatusContainer")), Times.Once);

            var unreadIds = _repository.GetUnreadMessageIds();
            var expectedList = new List<Guid> { savedUnreadIds[1] };

            Assert.Equal(expectedList, unreadIds);
        }

        [Fact]
        public void SetAsReadLoadsFromStoreOnceTest()
        {
            var savedUnreadIds = _fakeMessageStatusContainer.UnreadMessages.ToList();

            _repository.SetAsRead(savedUnreadIds[0]);
            _repository.SetAsRead(savedUnreadIds[1]);
            _repository.SetAsRead(Guid.NewGuid());
            _repository.SetAsRead(Guid.NewGuid());

            _mockMessageStatusStore.Verify(x => x.Get(It.Is<string>(y => y == "MessageStatusContainer")), Times.Once);
        }

        [Fact]
        public void SetAsReadSavesChangesToCacheTest()
        {
            var savedUnreadIds = _fakeMessageStatusContainer.UnreadMessages.ToList();

            _repository.SetAsRead(Guid.NewGuid());
            Assert.Equal(savedUnreadIds, _repository.GetUnreadMessageIds());

            _repository.SetAsRead(savedUnreadIds[0]);
            Assert.Equal(new List<Guid> { savedUnreadIds[1] }, _repository.GetUnreadMessageIds());

            _repository.SetAsRead(savedUnreadIds[1]);
            Assert.Empty(_repository.GetUnreadMessageIds());

            _repository.SetAsRead(Guid.NewGuid());
            Assert.Empty(_repository.GetUnreadMessageIds());
        }

        [Fact]
        public void SetAsReadSavesChangesToStoreTest()
        {
            var savedUnreadIds = _fakeMessageStatusContainer.UnreadMessages.ToList();

            _repository.SetAsRead(Guid.NewGuid());
            _mockMessageStatusStore.Verify(
                x => x.Put(
                    It.Is<MessageStatusContainer>(
                        y => y.UnreadMessages.SetEquals(savedUnreadIds.ToHashSet()))),
                    Times.Once);

            _repository.SetAsRead(savedUnreadIds[0]);
            _mockMessageStatusStore.Verify(
                x => x.Put(
                    It.Is<MessageStatusContainer>(
                        y => y.UnreadMessages.SetEquals(new HashSet<Guid> { savedUnreadIds[1] }))),
                    Times.Once);

            _repository.SetAsRead(savedUnreadIds[1]);
            _mockMessageStatusStore.Verify(
                x => x.Put(
                    It.Is<MessageStatusContainer>(
                        y => y.UnreadMessages.SetEquals(new HashSet<Guid>()))),
                    Times.Once);

            _repository.SetAsRead(Guid.NewGuid());
            _mockMessageStatusStore.Verify(
                x => x.Put(
                    It.Is<MessageStatusContainer>(
                        y => y.UnreadMessages.SetEquals(new HashSet<Guid>()))),
                    Times.Exactly(2));
        }

        [Fact]
        public void SetAsReadSavesChangesWhenObjectDoesntExistTest()
        {
            _mockMessageStatusStore.Setup(x => x.Get(It.Is<string>(y => y == "MessageStatusContainer")))
                .ReturnsAsync((MessageStatusContainer)null);

            _repository.SetAsRead(Guid.NewGuid());
            _mockMessageStatusStore.Verify(
                x => x.Put(
                    It.Is<MessageStatusContainer>(
                        y => y.Id == "MessageStatusContainer" && y.UnreadMessages.SetEquals(new HashSet<Guid>()))),
                    Times.Once);
        }

        [Fact]
        public void SetAsUnreadLoadsIdsFromStoreIfCacheNotInitializedTest()
        {
            var unreadId = Guid.NewGuid();
            _repository.SetAsUnread(unreadId);
            _mockMessageStatusStore.Verify(x => x.Get(It.Is<string>(y => y == "MessageStatusContainer")), Times.Once);

            var unreadIds = _repository.GetUnreadMessageIds();
            var expectedList = new List<Guid>(_fakeMessageStatusContainer.UnreadMessages);
            expectedList.Add(unreadId);

            Assert.Equal(expectedList, unreadIds);
        }

        [Fact]
        public void SetAsUnreadDoesNotLoadIdsFromStoreIfCacheInitializedTest()
        {
            // warm up cache and verify call
            _repository.GetUnreadMessageIds();
            _mockMessageStatusStore.Verify(x => x.Get(It.Is<string>(y => y == "MessageStatusContainer")), Times.Once);

            var unreadId = Guid.NewGuid();

            _repository.SetAsUnread(unreadId);

            // still one invocation
            _mockMessageStatusStore.Verify(x => x.Get(It.Is<string>(y => y == "MessageStatusContainer")), Times.Once);

            var unreadIds = _repository.GetUnreadMessageIds();
            var expectedList = new List<Guid>(_fakeMessageStatusContainer.UnreadMessages);
            expectedList.Add(unreadId);

            Assert.Equal(expectedList, unreadIds);
        }

        [Fact]
        public void SetAsUnreadLoadsFromStoreOnceTest()
        {
            var repeatGuid = Guid.NewGuid();
            _repository.SetAsUnread(Guid.NewGuid());
            _repository.SetAsUnread(Guid.NewGuid());
            _repository.SetAsUnread(repeatGuid);
            _repository.SetAsUnread(repeatGuid);

            _mockMessageStatusStore.Verify(x => x.Get(It.Is<string>(y => y == "MessageStatusContainer")), Times.Once);
        }

        [Fact]
        public void SetAsUnreadSavesChangesToCacheTest()
        {
            var savedUnreadIds = _fakeMessageStatusContainer.UnreadMessages.ToList();
            var guid1 = Guid.NewGuid();
            var guid2 = Guid.NewGuid();
            var repeatGuid = Guid.NewGuid();

            _repository.SetAsUnread(guid1);
            Assert.Equal(savedUnreadIds.Concat(new List<Guid> { guid1 }), _repository.GetUnreadMessageIds());

            _repository.SetAsUnread(guid2);
            Assert.Equal(savedUnreadIds.Concat(new List<Guid> { guid1, guid2 }), _repository.GetUnreadMessageIds());

            _repository.SetAsUnread(repeatGuid);
            Assert.Equal(
                savedUnreadIds.Concat(new List<Guid> { guid1, guid2, repeatGuid }),
                _repository.GetUnreadMessageIds());

            _repository.SetAsUnread(repeatGuid);
            Assert.Equal(
                savedUnreadIds.Concat(new List<Guid> { guid1, guid2, repeatGuid }),
                _repository.GetUnreadMessageIds());
        }

        [Fact]
        public void SetAsUnreadSavesChangesToStoreTest()
        {
            var savedUnreadIds = _fakeMessageStatusContainer.UnreadMessages.ToList();
            var guid1 = Guid.NewGuid();
            var guid2 = Guid.NewGuid();
            var repeatGuid = Guid.NewGuid();

            _repository.SetAsUnread(guid1);
            _mockMessageStatusStore.Verify(
                x => x.Put(
                    It.Is<MessageStatusContainer>(
                        y => y.UnreadMessages.SetEquals(savedUnreadIds.Concat(new List<Guid> { guid1 }).ToHashSet()))),
                    Times.Once);

            _repository.SetAsUnread(guid2);
            _mockMessageStatusStore.Verify(
                x => x.Put(
                    It.Is<MessageStatusContainer>(
                        y => y.UnreadMessages
                            .SetEquals(savedUnreadIds.Concat(new List<Guid> { guid1, guid2 }).ToHashSet()))),
                    Times.Once);

            _repository.SetAsUnread(repeatGuid);
            _mockMessageStatusStore.Verify(
                x => x.Put(
                    It.Is<MessageStatusContainer>(
                        y => y.UnreadMessages
                            .SetEquals(savedUnreadIds.Concat(new List<Guid> { guid1, guid2, repeatGuid }).ToHashSet()))),
                    Times.Once);

            _repository.SetAsUnread(repeatGuid);
            _mockMessageStatusStore.Verify(
                x => x.Put(
                    It.Is<MessageStatusContainer>(
                        y => y.UnreadMessages
                            .SetEquals(savedUnreadIds.Concat(new List<Guid> { guid1, guid2, repeatGuid }).ToHashSet()))),
                    Times.Exactly(2));
        }

        [Fact]
        public void SetAsUnreadSavesChangesWhenObjectDoesntExistTest()
        {
            _mockMessageStatusStore.Setup(x => x.Get(It.Is<string>(y => y == "MessageStatusContainer")))
                .ReturnsAsync((MessageStatusContainer)null);

            Guid guid = Guid.NewGuid();
            _repository.SetAsUnread(guid);
            _mockMessageStatusStore.Verify(
                x => x.Put(
                    It.Is<MessageStatusContainer>(
                        y => y.Id == "MessageStatusContainer" && y.UnreadMessages.SetEquals(new HashSet<Guid> { guid }))),
                    Times.Once);
        }
    }
}