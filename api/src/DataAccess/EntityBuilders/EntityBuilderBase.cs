namespace DataAccess.EntityBuilders
{   
    using Business.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Tools;

    public abstract class EntityBuilderBase<T> : IEntityBuilder
        where T : class, IHasId
    {
        private readonly ModelBuilder _modelBuilder;

        protected EntityBuilderBase(ModelBuilder modelBuilder)
        {
            Guard.NotNull(modelBuilder, nameof(modelBuilder));

            _modelBuilder = modelBuilder;
        }

        public void Build()
        {
            var typeBuilder = _modelBuilder.Entity<T>();

            typeBuilder.HasKey(x => x.Id);
            ConfigureEntity(typeBuilder);
        }

        protected virtual void ConfigureEntity(EntityTypeBuilder<T> typeBuilder)
        {
        }
    }
}