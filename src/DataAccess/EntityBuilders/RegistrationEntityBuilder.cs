namespace DetroitHarps.DataAccess.EntityBuilders
{
    using DetroitHarps.Business.Registration.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Tools;

    public class RegistrationEntityBuilder : EntityBuilderBase<Registration>
    {
        public RegistrationEntityBuilder(ModelBuilder modelBuilder)
            : base(modelBuilder)
        {
        }

        protected override void ConfigureEntity(EntityTypeBuilder<Registration> typeBuilder)
        {
            typeBuilder.OwnsOne(
                x => x.Parent,
                x =>
                {
                    x.Property(y => y.FirstName).HasMaxLength(Limits.NameLength).IsRequired();
                    x.Property(y => y.LastName).HasMaxLength(Limits.NameLength).IsRequired();
                    x.ToTable(nameof(Registration));
                });
            typeBuilder.OwnsOne(
                x => x.ContactInformation,
                x =>
                {
                    x.Property(y => y.Email).HasMaxLength(Limits.EmailLength).IsRequired();
                    x.Property(y => y.PhoneNumber).HasMaxLength(Limits.PhoneNumberLength).IsRequired();
                    x.Property(y => y.Address).HasMaxLength(Limits.AddressLength).IsRequired();
                    x.Property(y => y.Address2).HasMaxLength(Limits.AddressLength).IsRequired(false);
                    x.Property(y => y.City).HasMaxLength(Limits.CityLength).IsRequired();
                    x.Property(y => y.State).HasMaxLength(Limits.StateLength).IsRequired();
                    x.Property(y => y.Zip).HasMaxLength(Limits.ZipLength).IsRequired();
                    x.ToTable(nameof(Registration));
                });

            typeBuilder.OwnsOne(
                x => x.PaymentInformation,
                x => x.ToTable(nameof(Registration)));

            typeBuilder.HasMany(x => x.Children);
        }
    }
}