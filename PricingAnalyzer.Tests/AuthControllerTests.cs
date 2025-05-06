using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pricing_analyzer_back.Controllers;
using pricing_analyzer_back.Infrasctructure.Context;
using pricing_analyzer_back.Infrasctructure.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PricingAnalyzer.Tests
{
    public class AuthControllerTests
    {
        private ApplicationDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("TestDb_Users")
                .Options;

            return new ApplicationDbContext(options);
        }

        [Fact]
        public async Task Register_CreatesNewUser()
        {
            // Arrange
            var context = GetDbContext();
            var controller = new AuthController(context);

            var dto = new RegisterDto
            {
                Username = "testuser",
                Password = "12345"
            };

            // Act
            var result = await controller.Register(dto);
            var createdResult = Assert.IsType<OkObjectResult>(result);
            var user = createdResult.Value as dynamic;

            // Assert
            Assert.NotNull(user);
            Assert.Equal("testuser", (string)user.Username);
        }
    }
}
