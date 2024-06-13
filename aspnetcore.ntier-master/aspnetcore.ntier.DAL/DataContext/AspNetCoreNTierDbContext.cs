using aspnetcore.ntier.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace aspnetcore.ntier.DAL.DataContext;

public class AspNetCoreNTierDbContext : DbContext
{
    public AspNetCoreNTierDbContext(DbContextOptions<AspNetCoreNTierDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<ShortUrl> ShortUrls { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ShortUrl>()
            .HasOne(s => s.CreatedBy)
            .WithMany(u => u.ShortUrls)
            .HasForeignKey(s => s.CreatedById)
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<User>()
            .HasMany(u => u.UserRoles)
            .WithOne(ur => ur.User)
            .HasForeignKey(ur => ur.UserId);

        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
            .HasKey(u => u.UserId);
        modelBuilder.Entity<User>().HasData(
             new User
             {
                 UserId = 1,
                 Name = "Ali",
                 Username = "Admin",
                 Surname="Raza",
                 Password = "123",
                IsAdmin = true,
             }
         );
    }
}