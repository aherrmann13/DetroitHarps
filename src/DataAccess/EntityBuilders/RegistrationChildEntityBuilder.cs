namespace DetroitHarps.DataAccess.EntityBuilders
{
    using DetroitHarps.Business;
    using DetroitHarps.Business.Registration.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Tools;

    public class RegistrationChildEntityBuilder : EntityBuilderBase<RegistrationChild>
    {
        public RegistrationChildEntityBuilder(ModelBuilder modelBuilder)
            : base(modelBuilder)
        {
        }

        protected override void ConfigureEntity(EntityTypeBuilder<RegistrationChild> typeBuilder)
        {
            typeBuilder.Property(x => x.FirstName).HasMaxLength(Limits.NameLength).IsRequired(true);
            typeBuilder.Property(x => x.LastName).HasMaxLength(Limits.NameLength).IsRequired(true);

            // TODO: this needs to be done with annotations not a hardcoded column type
            typeBuilder.Property(x => x.DateOfBirth).HasColumnType("date");

            typeBuilder.Property(x => x.ShirtSize).HasMaxLength(Limits.ShirtSizeLength);

            typeBuilder.HasMany(x => x.Events);
        }
    }
}