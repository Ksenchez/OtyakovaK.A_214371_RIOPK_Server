using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pricing_analyzer_back.Controllers;
using pricing_analyzer_back.Infrasctructure.Context;
using pricing_analyzer_back.Infrasctructure.Models.Dto;
using pricing_analyzer_back.Infrasctructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PricingAnalyzer.Tests
{
    public class ProductCalculationsTests
    {
        private ApplicationDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("TestDb_Calculations")
                .Options;

            return new ApplicationDbContext(options);
        }

        [Fact]
        public async Task Calculate_SavesCalculation()
        {
            // Arrange
            var context = GetDbContext();
            var product = new Product { Name = "Test", BaseCost = 100, MarkupPercent = 10, Description = "test desc", CreatedAt = DateTime.UtcNow };
            var user = new User { Username = "user", PasswordHash = "hash", Role = "user" };

            context.Products.Add(product);
            context.Users.Add(user);
            await context.SaveChangesAsync();

            var controller = new ProductCalculationsController(context);
            var dto = new CalculationDto
            {
                ProductId = product.Id,
                UserId = user.Id,
                CustomMarkup = 15
            };

            // Act
            var result = await controller.Calculate(dto);
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);

            // Assert
            Assert.NotNull(createdResult.Value);
        }
    }
}
