namespace DetroitHarps.DataAccess.EntityBuilders
{
    using DetroitHarps.Business;
    using DetroitHarps.Business.Registration.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Tools;

    public class RegistrationChildEventEntityBuilder
        : EntityBuilderBase<RegistrationChildEvent>
    {
        public RegistrationChildEventEntityBuilder(ModelBuilder modelBuilder)
            : base(modelBuilder)
        {
        }

        protected override void ConfigureEntity(
            EntityTypeBuilder<RegistrationChildEvent> typeBuilder)
        {
            typeBuilder.OwnsOne(
                x => x.EventSnapshot,
                x => x.ToTable(nameof(RegistrationChildEvent)));
        }
    }
}