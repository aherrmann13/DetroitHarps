namespace DetroitHarps.DataAccess.EntityBuilders
{
    using System;
    using DetroitHarps.Business;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Tools;

    // TODO: genericise `IHasId<int>`?
    // causes problems with `dbContext.ChangeTracker.Entries<IHasId<int>>()`
    // C# equivalent of [_] in scala
    public abstract class EntityBuilderBase<T> : IEntityBuilder
        where T : class, IHasId<int>
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

            AddKey(typeBuilder);
            AddAuditProperties(typeBuilder);

            ConfigureEntity(typeBuilder);
        }

        protected virtual void ConfigureEntity(EntityTypeBuilder<T> typeBuilder)
        {
        }

        private static void AddKey(EntityTypeBuilder<T> typeBuilder)
        {
            typeBuilder.HasKey(x => x.Id);
        }

        private static void AddAuditProperties(EntityTypeBuilder<T> typeBuilder)
        {
            typeBuilder.Property<DateTimeOffset>(Constants.InsertTimestampPropertyName);
            typeBuilder.Property<DateTimeOffset>(Constants.UpdateTimestampPropertyName);
        }
    }
}
