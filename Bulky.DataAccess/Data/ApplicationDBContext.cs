using Bulky.Models;
using Microsoft.EntityFrameworkCore;

namespace Bulky.DataAccess.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Seeding
        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Action", DisplayOrder = 1 },
            new Category { Id = 2, Name = "SciFi", DisplayOrder = 2 },
            new Category { Id = 3, Name = "History", DisplayOrder = 3 }
        );

        modelBuilder.Entity<Product>().HasData(
            new Product
            {
                Id = 1,
                Title = "A",
                Author = "a",
                Description = "A description",
                ISBN = "SWD",
                ListPrice = 99,
                Price = 100,
                Price50 = 85,
                Price100 = 80,
                CategoryId = 1
            },
            new Product
            {
                Id = 2,
                Title = "A",
                Author = "a",
                Description = "A description",
                ISBN = "SWD",
                ListPrice = 99,
                Price = 100,
                Price50 = 85,
                Price100 = 80,
                CategoryId = 1
            },
            new Product
            {
                Id = 3,
                Title = "A",
                Author = "a",
                Description = "A description",
                ISBN = "SWD",
                ListPrice = 99,
                Price = 100,
                Price50 = 85,
                Price100 = 80,
                CategoryId = 1
            },
            new Product
            {
                Id = 4,
                Title = "A",
                Author = "a",
                Description = "A description",
                ISBN = "SWD",
                ListPrice = 99,
                Price = 100,
                Price50 = 85,
                Price100 = 80,
                CategoryId = 1
            }
        );
    }
}