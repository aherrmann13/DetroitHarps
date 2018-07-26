namespace DataAccess.EntityBuilders
{   
    using Business.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Tools;

    public class PhotoDisplayPropertiesEntityBuilder : EntityBuilderBase<PhotoDisplayProperties>
    {
        public PhotoDisplayPropertiesEntityBuilder(ModelBuilder modelBuilder)
            : base(modelBuilder)
        {
        }

        protected override void ConfigureEntity(EntityTypeBuilder<PhotoDisplayProperties> typeBuilder)
        {
            typeBuilder.HasOne(x => x.PhotoData);
        }
    }
}