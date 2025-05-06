using Microsoft.EntityFrameworkCore;
using pricing_analyzer_back.Infrasctructure.Models;

namespace pricing_analyzer_back.Infrasctructure.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; } = default!;
        public DbSet<Product> Products { get; set; } = default!;
        public DbSet<PricingPolicy> PricingPolicies { get; set; } = default!;
        public DbSet<ProductCalculation> ProductCalculations { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Пользователи
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Username = "admin",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin"),
                    Role = "admin"
                },
                new User
                {
                    Id = 2,
                    Username = "user",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("user"),
                    Role = "user"
                }
            );

            // Политики ценообразования
            modelBuilder.Entity<PricingPolicy>().HasData(
                new PricingPolicy { Id = 1, PolicyName = "Стандартная", Description = "Базовая наценка 10%", DefaultMarkupPercent = 10 },
                new PricingPolicy { Id = 2, PolicyName = "Премиум", Description = "Увеличенная наценка 20%", DefaultMarkupPercent = 20 },
                new PricingPolicy { Id = 3, PolicyName = "Льготная", Description = "Минимальная наценка 5%", DefaultMarkupPercent = 5 }
            );

            // Продукты
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "Молоко",
                    Description = "Молоко 950мл.",
                    BaseCost = 100,
                    MarkupPercent = 15,
                    CreatedAt = DateTime.UtcNow,
                    PricingPolicyId = null
                },
                new Product
                {
                    Id = 2,
                    Name = "Масло",
                    Description = "Масло 200гр.",
                    BaseCost = 200,
                    MarkupPercent = 10,
                    CreatedAt = DateTime.UtcNow,
                    PricingPolicyId = 1
                },
                new Product
                {
                    Id = 3,
                    Name = "Coca-Cola",
                    Description = "Coca-Cola 2л.",
                    BaseCost = 150,
                    MarkupPercent = 20,
                    CreatedAt = DateTime.UtcNow,
                    PricingPolicyId = 2
                }
            );
        }

    }
}
