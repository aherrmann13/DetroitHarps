namespace DetroitHarps.DataAccess.EntityBuilders
{
    using DetroitHarps.Business;
    using DetroitHarps.Business.Registration.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Tools;

    public class RegistrationChildEntityBuilder : IEntityBuilder
    {
        private const string IdColumnName = nameof(IHasId.Id);

        private readonly ModelBuilder _modelBuilder;

        public RegistrationChildEntityBuilder(ModelBuilder modelBuilder)
        {
            Guard.NotNull(modelBuilder, nameof(modelBuilder));

            _modelBuilder = modelBuilder;
        }

        public void Build()
        {
            var typeBuilder = _modelBuilder.Entity<RegistrationChild>();

            typeBuilder.Property<int>(IdColumnName);
            typeBuilder.HasKey(IdColumnName);

            typeBuilder.Property(x => x.FirstName).HasMaxLength(Limits.NameLength).IsRequired(true);
            typeBuilder.Property(x => x.LastName).HasMaxLength(Limits.NameLength).IsRequired(true);

            // TODO: this needs to be done with annotations not a hardcoded column type
            typeBuilder.Property(x => x.DateOfBirth).HasColumnType("date");

            typeBuilder.Property(x => x.ShirtSize).HasMaxLength(Limits.ShirtSizeLength);
        }
    }
}