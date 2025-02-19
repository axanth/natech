using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

using Domain;

namespace Infrastructure.Data
{
    public class CatsDbContext(DbContextOptions<CatsDbContext> options) : DbContext(options)
    {
        public DbSet<Cat> Cats { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (EntityEntry<BaseEntity> entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = DateTime.UtcNow;
                        //entry.Entity.CreateDate = _dateTime.Now;
                        break;
                }
            }

            var result = await base.SaveChangesAsync(cancellationToken);
            return result;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cat>()
                .HasMany(c => c.Tags)
                .WithMany(t => t.Cats)
                .UsingEntity<Dictionary<string, object>>(
                    "CatTag", // Name of the junction table in DB
                    j => j.HasOne<Tag>().WithMany().HasForeignKey("TagId"),
                    j => j.HasOne<Cat>().WithMany().HasForeignKey("CatId")
                );

            modelBuilder.Entity<Cat>()
              .HasIndex(c => c.CatId).IsUnique();

            modelBuilder.Entity<Tag>()
             .HasIndex(c => c.Name).IsUnique();

        }
    }
}