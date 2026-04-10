using GrapheneCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrapheneCore.Database;

public class GrapheneCoreDbContext : DbContext
{
    public GrapheneCoreDbContext(DbContextOptions<GrapheneCoreDbContext> options)
        : base(options)
    {
        //
    }

    public DbSet<Product> Products => Set<Product>();

    public DbSet<Order> Orders => Set<Order>();

    public IQueryable<dynamic>? GetSet<EntityType>(string entityName) where EntityType : class
    {
        switch (entityName.ToLower())
        {
            case "products":
                return Products;
            case "orders":
                return Orders;
            default:
                return null;
        }
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    => optionsBuilder.LogTo(Console.WriteLine)
            .EnableSensitiveDataLogging();


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder); // Always call base.OnModelCreating
        // Seed a related entity (requires specifying the foreign key value)
        modelBuilder.Entity<Product>().HasData(
            new Product { Id = 1, Name = "Espresso", Description = "A strong coffee made by forcing steam through ground coffee beans.", Price = 2.50m },
            new Product { Id = 2, Name = "Cappuccino", Description = "A coffee drink that is traditionally prepared with espresso, hot milk, and steamed milk foam.", Price = 3.50m },
            new Product { Id = 3, Name = "Latte", Description = "A coffee drink made with espresso and steamed milk.", Price = 3.00m },
            new Product { Id = 4, Name = "Americano", Description = "A coffee drink made by diluting an espresso with hot water.", Price = 2.00m },
            new Product { Id = 5, Name = "Mocha", Description = "A chocolate-flavored variant of a latte.", Price = 3.75m },
            new Product { Id = 6, Name = "Macchiato", Description = "An espresso coffee drink with a small amount of milk, usually foamed.", Price = 2.75m },
            new Product { Id = 7, Name = "Flat White", Description = "A coffee drink consisting of espresso with microfoam (steamed milk with small, fine bubbles) and little to no foam.", Price = 3.25m }
        );

        modelBuilder.Entity<Order>().HasData(
            new Order { Id = 1, Tag = "Order1", OrderedAt = DateTime.UtcNow.AddDays(-1), ProductId = 1 },
            new Order { Id = 2, Tag = "Order2", OrderedAt = DateTime.UtcNow.AddDays(-2), ProductId = 2 },
            new Order { Id = 3, Tag = "Order3", OrderedAt = DateTime.UtcNow.AddDays(-3), ProductId = 3 },
            new Order { Id = 4, Tag = "Order4", OrderedAt = DateTime.UtcNow.AddDays(-4), ProductId = 4 },
            new Order { Id = 5, Tag = "Order5", OrderedAt = DateTime.UtcNow.AddDays(-5), ProductId = 5 }
        );
    }
}
