namespace DetroitHarps.DataAccess.EntityBuilders
{
    using DetroitHarps.Business.Photo.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Tools;

    public class PhotoGroupEntityBuilder : EntityBuilderBase<PhotoGroup>
    {
        public PhotoGroupEntityBuilder(ModelBuilder modelBuilder)
            : base(modelBuilder)
        {
        }

        protected override void ConfigureEntity(EntityTypeBuilder<PhotoGroup> typeBuilder)
        {
            typeBuilder.Property(x => x.Name).HasMaxLength(Limits.NameLength).IsRequired();
        }
    }
}