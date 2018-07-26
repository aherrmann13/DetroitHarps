namespace DataAccess.EntityBuilders
{   
    using Business.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Tools;

    public class PhotoDataEntityBuilder : EntityBuilderBase<PhotoData>
    {
        public PhotoDataEntityBuilder(ModelBuilder modelBuilder)
            : base(modelBuilder)
        {
        }
    }
}