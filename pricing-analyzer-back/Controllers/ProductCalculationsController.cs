using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pricing_analyzer_back.Infrasctructure.Context;
using pricing_analyzer_back.Infrasctructure.Models;
using pricing_analyzer_back.Infrasctructure.Models.Dto;
using System;

namespace pricing_analyzer_back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCalculationsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductCalculationsController(ApplicationDbContext context) => _context = context;

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(await _context.ProductCalculations
                .Include(c => c.Product)
                .Include(c => c.User)
                .ToListAsync());

        [HttpPost]
        public async Task<IActionResult> Calculate([FromBody] CalculationDto dto)
        {
            var product = await _context.Products.FindAsync(dto.ProductId);
            var user = await _context.Users.FindAsync(dto.UserId);
            if (product == null || user == null)
                return BadRequest("Неверный продукт или пользователь");

            var calc = new ProductCalculation
            {
                ProductId = dto.ProductId,
                UserId = dto.UserId,
                CustomMarkup = dto.CustomMarkup,
                CalculatedAt = DateTime.UtcNow
            };

            _context.ProductCalculations.Add(calc);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAll), new { id = calc.Id }, calc);
        }

        [HttpGet("price-history/{productId}")]
        public async Task<IActionResult> GetPriceHistory(int productId)
        {
            var history = await _context.ProductCalculations
                .Where(c => c.ProductId == productId)
                .OrderBy(c => c.CalculatedAt)
               .Select(c => new {
                   Date = c.CalculatedAt,
                   CustomMarkup = c.CustomMarkup,
                   Price = c.Product.BaseCost * (1 + c.CustomMarkup / 100)
               })
                .ToListAsync();

            return Ok(history);
        }

        [HttpGet("profitability-report")]
        public async Task<IActionResult> GetProfitabilityReport()
        {
            var products = await _context.Products
                .Include(p => p.PricingPolicy)
                .ToListAsync();

            var calculations = await _context.ProductCalculations
                .Include(c => c.Product)
                .ToListAsync();

            var report = products.Select(p =>
            {
                var relatedCalcs = calculations.Where(c => c.ProductId == p.Id).ToList();

                var avgMarkup = relatedCalcs.Any() ? relatedCalcs.Average(c => (double)c.CustomMarkup) : 0;
                var avgPrice = relatedCalcs.Any()
                    ? relatedCalcs.Average(c => (double)(c.Product.BaseCost * (1 + c.CustomMarkup / 100)))
                    : (double)(p.BaseCost * (1 + p.MarkupPercent / 100));

                var profitPerUnit = avgPrice - (double)p.BaseCost;
                var count = relatedCalcs.Count;

                return new
                {
                    ProductId = p.Id,
                    ProductName = p.Name,
                    BaseCost = p.BaseCost,
                    AverageMarkup = avgMarkup,
                    AveragePrice = avgPrice,
                    Count = count,
                    ProfitPerUnit = profitPerUnit,
                    TotalProfit = profitPerUnit * count
                };
            })
            .OrderBy(r => r.TotalProfit)
            .ToList();

            return Ok(report);
        }



    }
}
