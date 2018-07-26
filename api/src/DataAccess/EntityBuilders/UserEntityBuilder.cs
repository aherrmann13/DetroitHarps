namespace DataAccess.EntityBuilders
{   
    using Business.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Tools;

    public class UserEntityBuilder : EntityBuilderBase<User>
    {
        public UserEntityBuilder(ModelBuilder modelBuilder)
            : base(modelBuilder)
        {
        }
    }
}