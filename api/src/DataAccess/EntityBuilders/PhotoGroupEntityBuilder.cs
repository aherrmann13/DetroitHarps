namespace DataAccess.EntityBuilders
{   
    using Business.Entities;
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
            typeBuilder.HasMany(x => x.Photos)
                .WithOne()
                .HasForeignKey(x => x.PhotoGroupId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}