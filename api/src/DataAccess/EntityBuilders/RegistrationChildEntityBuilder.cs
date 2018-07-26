namespace DataAccess.EntityBuilders
{   
    using Business.Entities;
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
            typeBuilder.Property(x=> x.DateOfBirth)
                .HasColumnType("date");
        }
    }
}