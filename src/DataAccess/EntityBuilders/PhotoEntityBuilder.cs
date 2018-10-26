namespace DetroitHarps.DataAccess.EntityBuilders
{
    using DetroitHarps.Business.Photo.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Tools;

    public class PhotoEntityBuilder : EntityBuilderBase<Photo>
    {
        public PhotoEntityBuilder(ModelBuilder modelBuilder)
            : base(modelBuilder)
        {
        }

        protected override void ConfigureEntity(EntityTypeBuilder<Photo> typeBuilder)
        {
            typeBuilder.OwnsOne(
                x => x.DisplayProperties,
                x =>
                {
                    x.Property(y => y.Title).HasMaxLength(Limits.NameLength).IsRequired();
                    x.HasOne<PhotoGroup>()
                        .WithMany()
                        .HasForeignKey(y => y.PhotoGroupId)
                        .OnDelete(DeleteBehavior.Restrict);
                    x.ToTable(nameof(Photo));
                });
            typeBuilder.OwnsOne(
                x => x.Data,
                x =>
                {
                    x.Property(y => y.MimeType).HasMaxLength(Limits.MimeTypeLength);
                    x.ToTable(nameof(PhotoData));
                });
        }
    }
}