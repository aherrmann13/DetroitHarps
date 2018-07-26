namespace DataAccess.EntityBuilders
{   
    using Business.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Tools;

    public class RegistrationPaymentInformationEntityBuilder : EntityBuilderBase<RegistrationPaymentInformation>
    {
        public RegistrationPaymentInformationEntityBuilder(ModelBuilder modelBuilder)
            : base(modelBuilder)
        {
        }
    }
}