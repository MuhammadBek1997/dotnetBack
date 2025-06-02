using FudballManagement.Domain.Entities.Order;
using FudballManagement.Domain.Entities.Stadiums;
using FudballManagement.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace FudballManagement.Infrastructure.DbContexts;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        Database.Migrate();
    }
    #region models
    public DbSet<Orders> Orders { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Admin> Admins  { get; set; }
    public DbSet<Stadium> Stadiums { get; set;}
    public DbSet<StadiumComment> stadiumComments { get; set; }
    public DbSet<StadiumMedia> stadiumMedias { get; set; }
    public DbSet<StadiumRating> stadiumRatings { get;set; }
    #endregion
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        modelBuilder.Entity<Stadium>()
            .HasOne(s => s.Admin)
            .WithMany(s => s.Stadiums)
            .HasForeignKey(s => s.AdminId);

        modelBuilder.Entity<StadiumRating>()
            .HasOne(s => s.Stadium)
            .WithMany(s => s.stadiumRatings)
            .HasForeignKey(r => r.StadiumId);

        modelBuilder.Entity<StadiumMedia>()
            .HasOne(s => s.Stadium)
            .WithMany(s => s.StadiumMedias)
            .HasForeignKey(s => s.StadiumId);

        modelBuilder.Entity<StadiumComment>()
            .HasOne(sc => sc.Stadium)
            .WithMany(s => s.StadiumComments)
            .HasForeignKey(sc => sc.StadiumId);

        modelBuilder.Entity<StadiumComment>()
            .HasOne(sc => sc.Customer)
            .WithMany(sc => sc.stadiumComments)
            .HasForeignKey(sc => sc.CustomerId);

        modelBuilder.Entity<StadiumRating>()
            .HasOne(sr => sr.Customer)
            .WithMany(sr => sr.StadiumRatings)
            .HasForeignKey(sr => sr.CustomerId);

        modelBuilder.Entity<Admin>()
            .HasIndex(a => a.PhoneNumber)
            .IsUnique();

        modelBuilder.Entity<Customer>()
           .HasIndex(a => a.PhoneNumber)
           .IsUnique();

        modelBuilder.Entity<Stadium>()
           .HasIndex(s => s.StadiumName)
           .IsUnique();
    }

    
}
