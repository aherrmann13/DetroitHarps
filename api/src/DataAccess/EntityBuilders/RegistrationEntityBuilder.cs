namespace DataAccess.EntityBuilders
{   
    using Business.Entities;
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
            typeBuilder.HasOne(x => x.Parent);

            typeBuilder.HasOne(x => x.ContactInformation);

            typeBuilder.HasOne(x => x.PaymentInformation);
            
            typeBuilder.HasMany(x => x.Children);
        }
    }
}