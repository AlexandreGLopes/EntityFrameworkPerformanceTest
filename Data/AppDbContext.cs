using EFPerformanceTest.Models;
using Microsoft.EntityFrameworkCore;

namespace EFPerformanceTest.Data;

public sealed class AppDbContext : DbContext
{
    public DbSet<Person> Person { get; set; }
    public DbSet<Address> Address { get; set; }
    public DbSet<Graduation> Graduation { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //optionsBuilder.UseSqlite("Data Source=EFPerformanceTest.db",
        //    options => options.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName));

        optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=EFPerformanceTestDb;Trusted_Connection=True;",
            options => options.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>()
            .HasOne(p => p.Address)
            .WithOne()
            .IsRequired(false)
            .HasForeignKey<Person>(p => p.AddressId)
            .HasConstraintName("FK_Person_Address")
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Person>()
            .HasMany(p => p.Graduations)
            .WithMany(g => g.Persons)
            .UsingEntity<Dictionary<string, object>>("PersonsGraduations", config =>
            {
                 config.HasOne<Person>().WithMany().HasForeignKey("PersonId");
                 config.HasOne<Graduation>().WithMany().HasForeignKey("GraduationId");
            });
    }
}
