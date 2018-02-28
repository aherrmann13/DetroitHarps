namespace Repository
{
    using Microsoft.EntityFrameworkCore;
    using Repository.Entities;
    
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) 
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RegisteredPerson>()
                .HasOne(x => x.Season)
                .WithMany(x => x.RegisteredPeople)
                .HasForeignKey(x => x.SeasonId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PaymentDetails>()
                .HasOne(x => x.RegisteredPerson)
                .WithMany(x => x.PaymentDetails)
                .HasForeignKey(x => x.RegisteredPersonId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RegisteredChild>()
                .HasOne(x => x.RegisteredPerson)
                .WithMany(x => x.Children)
                .HasForeignKey(x => x.RegisteredPersonId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Photo>()
                .HasOne(x => x.PhotoGroup)
                .WithMany(x => x.Photos)
                .HasForeignKey(x => x.PhotoGroupId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Event>();
            modelBuilder.Entity<Season>();
            modelBuilder.Entity<User>();
            modelBuilder.Entity<PhotoGroup>();
        }
    }
}