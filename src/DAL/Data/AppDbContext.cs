namespace DAL.Data;

using DAL.Entities;
using Microsoft.EntityFrameworkCore;

public sealed class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Product> Products => Set<Product>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.Price).HasColumnType("decimal(18,2)");
            entity.Property(e => e.Category).IsRequired().HasMaxLength(100);

            // Seed data
            entity.HasData(
                new Product { Id = 1, Name = "Laptop Dell XPS 15", Description = "Wydajny laptop do pracy i rozrywki", Price = 6499.99m, StockQuantity = 25, Category = "Elektronika", IsActive = true, CreatedAt = new DateTime(2025, 1, 15, 0, 0, 0, DateTimeKind.Utc) },
                new Product { Id = 2, Name = "Klawiatura mechaniczna Keychron K8", Description = "Bezprzewodowa klawiatura z przełącznikami Gateron", Price = 449.00m, StockQuantity = 50, Category = "Peryferia", IsActive = true, CreatedAt = new DateTime(2025, 2, 10, 0, 0, 0, DateTimeKind.Utc) },
                new Product { Id = 3, Name = "Monitor LG 27\" 4K", Description = "Monitor IPS z HDR10", Price = 1899.00m, StockQuantity = 15, Category = "Elektronika", IsActive = true, CreatedAt = new DateTime(2025, 3, 5, 0, 0, 0, DateTimeKind.Utc) },
                new Product { Id = 4, Name = "Słuchawki Sony WH-1000XM5", Description = "Bezprzewodowe słuchawki z ANC", Price = 1599.00m, StockQuantity = 30, Category = "Audio", IsActive = true, CreatedAt = new DateTime(2025, 1, 20, 0, 0, 0, DateTimeKind.Utc) },
                new Product { Id = 5, Name = "Mysz Logitech MX Master 3S", Description = "Ergonomiczna mysz bezprzewodowa", Price = 499.00m, StockQuantity = 40, Category = "Peryferia", IsActive = false, CreatedAt = new DateTime(2025, 4, 1, 0, 0, 0, DateTimeKind.Utc) }
            );
        });
    }
}
