namespace DataAccess.EntityBuilders
{   
    using Business.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Tools;

    public class RegistrationContactInformationEntityBuilder : EntityBuilderBase<RegistrationContactInformation>
    {
        public RegistrationContactInformationEntityBuilder(ModelBuilder modelBuilder)
            : base(modelBuilder)
        {
        }
    }
}