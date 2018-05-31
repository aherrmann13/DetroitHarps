namespace DataAccess.Dataloader.DataUnit
{
    using System.Collections.Generic;
    using System.Linq;

    public abstract class DataUnitBase<T> : IDataUnit
        where T : class
    {
        private readonly ApiDbContext _dbContext;

        public DataUnitBase(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected abstract IEnumerable<T> Data { get; }

        public void Run(bool clearExisting = true)
        {
            if(clearExisting)
            {
                ClearExistingEntries();
            }
            _dbContext.AddRange(Data);
            _dbContext.SaveChanges();
        }

        private void ClearExistingEntries()
        {
            var existingEntities = _dbContext.Set<T>().ToList();
            _dbContext.RemoveRange(existingEntities);
            _dbContext.SaveChanges();
        }
    }
}
