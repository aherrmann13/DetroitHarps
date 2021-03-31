namespace DetroitHarps.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DetroitHarps.Business.Contact;
    using DetroitHarps.DataAccess.S3;
    using DetroitHarps.Repository.Internal;

    public class MessageStatusRepository : S3RepositoryBase<MessageStatusContainer, string>, IMessageStatusRepository
    {
        private static readonly string MessageStatusContainerId = "MessageStatusContainer";
        private readonly ISet<Guid> _unreadIdCache = new HashSet<Guid>();
        private bool _cacheInitialized = false;

        public MessageStatusRepository(IS3ObjectStore<MessageStatusContainer, string> messageStatusStore)
            : base(messageStatusStore)
        {
        }

        public IList<Guid> GetUnreadMessageIds()
        {
            LoadStateFromStore();
            return _unreadIdCache.ToList();
        }

        public void SetAsRead(Guid messageId)
        {
            LoadStateFromStore();
            _unreadIdCache.Remove(messageId);
            UpdateState();
        }

        public void SetAsUnread(Guid messageId)
        {
            LoadStateFromStore();
            _unreadIdCache.Add(messageId);
            UpdateState();
        }

        private void LoadStateFromStore()
        {
            if (!_cacheInitialized)
            {
                var container = Store.Get(MessageStatusContainerId).Result;
                foreach (var item in container?.UnreadMessages ?? new HashSet<Guid>())
                {
                    _unreadIdCache.Add(item);
                }

                _cacheInitialized = true;
            }
        }

        // TODO: something to queue this job in an async ordered task runner
        private void UpdateState()
        {
            var newState = new MessageStatusContainer
            {
                Id = MessageStatusContainerId,
                UnreadMessages = new HashSet<Guid>(_unreadIdCache)
            };
            Store.Put(newState).Wait();
        }
    }
}