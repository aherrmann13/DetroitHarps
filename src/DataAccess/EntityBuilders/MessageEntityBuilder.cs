namespace DetroitHarps.DataAccess.EntityBuilders
{
    using DetroitHarps.Business.Contact.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Tools;

    public class MessageEntityBuilder : EntityBuilderBase<Message>
    {
        public MessageEntityBuilder(ModelBuilder modelBuilder)
            : base(modelBuilder)
        {
        }

        protected override void ConfigureEntity(EntityTypeBuilder<Message> typeBuilder)
        {
            typeBuilder.Property(x => x.FirstName).HasMaxLength(Limits.NameLength).IsRequired();
            typeBuilder.Property(x => x.LastName).HasMaxLength(Limits.NameLength).IsRequired();
            typeBuilder.Property(x => x.Email).HasMaxLength(Limits.EmailLength).IsRequired();
            typeBuilder.Property(x => x.Body).HasMaxLength(Limits.BodyLength).IsRequired();
        }
    }
}