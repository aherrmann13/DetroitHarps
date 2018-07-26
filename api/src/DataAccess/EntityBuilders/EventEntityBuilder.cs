namespace DataAccess.EntityBuilders
{   
    using Business.Entities;
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
            typeBuilder.Property(x=> x.Date)
                .HasColumnType("date");
        }
    }
}