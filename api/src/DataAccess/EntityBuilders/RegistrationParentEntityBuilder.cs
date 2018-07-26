namespace DataAccess.EntityBuilders
{   
    using Business.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Tools;

    public class RegistrationParentEntityBuilder : EntityBuilderBase<RegistrationParent>
    {
        public RegistrationParentEntityBuilder(ModelBuilder modelBuilder)
            : base(modelBuilder)
        {
        }
    }
}