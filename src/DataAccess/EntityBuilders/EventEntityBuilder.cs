namespace DetroitHarps.DataAccess.EntityBuilders
{
    using DetroitHarps.Business.Schedule.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Tools;

    public class EventEntityBuilder : EntityBuilderBase<Event>
    {
        public EventEntityBuilder(ModelBuilder modelBuilder)
            : base(modelBuilder)
        {
        }

        protected override void ConfigureEntity(EntityTypeBuilder<Event> typeBuilder)
        {
            typeBuilder.Property(x => x.Title).HasMaxLength(Limits.NameLength).IsRequired();
            typeBuilder.Property(x => x.Description).HasMaxLength(Limits.BodyLength).IsRequired();
        }
    }
}