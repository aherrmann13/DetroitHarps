namespace DetroitHarps.DataAccess.EntityBuilders
{
    using DetroitHarps.Business;
    using DetroitHarps.Business.Registration.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Tools;

    public class RegistrationChildEventEntityBuilder : IEntityBuilder
    {
        private const string IdColumnName = nameof(IHasId.Id);

        private readonly ModelBuilder _modelBuilder;

        public RegistrationChildEventEntityBuilder(ModelBuilder modelBuilder)
        {
            Guard.NotNull(modelBuilder, nameof(modelBuilder));

            _modelBuilder = modelBuilder;
        }

        // TODO: should this have audit props?
        public void Build()
        {
            var typeBuilder = _modelBuilder.Entity<RegistrationChildEvent>();

            typeBuilder.Property<int>(IdColumnName);
            typeBuilder.HasKey(IdColumnName);

            typeBuilder.OwnsOne(
                x => x.EventSnapshot,
                x => x.ToTable(nameof(RegistrationChildEvent)));
        }
    }
}