using Microsoft.EntityFrameworkCore;
using pricing_analyzer_back.Controllers;
using pricing_analyzer_back.Infrasctructure.Context;
using pricing_analyzer_back.Infrasctructure.Models;
using pricing_analyzer_back.Infrasctructure.Models.Dto;

namespace PricingAnalyzer.Tests
{
    public class ProductsControllerTests
    {
        private ApplicationDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb_Products")
                .Options;

            return new ApplicationDbContext(options);
        }

        [Fact]
        public async Task Create_AddsProductSuccessfully()
        {
            // Arrange
            var context = GetDbContext();
            var controller = new ProductsController(context);

            var product = new CreateProductDto
            {
                Name = "Тестовый продукт",
                Description = "Описание",
                BaseCost = 150,
                MarkupPercent = 10
            };

            // Act
            await controller.Create(product);
            var saved = await context.Products.FirstOrDefaultAsync(p => p.Name == "Тестовый продукт");

            // Assert
            Assert.NotNull(saved);
            Assert.Equal(150, saved.BaseCost);
            Assert.Equal("Описание", saved.Description);
        }
    }
}