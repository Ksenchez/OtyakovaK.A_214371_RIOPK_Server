using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pricing_analyzer_back.Infrasctructure.Context;
using pricing_analyzer_back.Infrasctructure.Models;
using pricing_analyzer_back.Infrasctructure.Models.Dto;
using System;

namespace pricing_analyzer_back.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context) => _context = context;

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var products = await _context.Products
                .Include(p => p.PricingPolicy)
                .ToListAsync();

            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var product = await _context.Products
                .Include(p => p.PricingPolicy)
                .FirstOrDefaultAsync(p => p.Id == id);

            return product == null ? NotFound() : Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductDto request)
        {
            decimal markup = request.MarkupPercent ?? 0;

            if (request.PricingPolicyId.HasValue)
            {
                var policy = await _context.PricingPolicies.FindAsync(request.PricingPolicyId.Value);
                if (policy == null)
                    return BadRequest("Политика не найдена");

                markup = policy.DefaultMarkupPercent;
            }

            var product = new Product
            {
                Name = request.Name,
                Description = request.Description,
                BaseCost = request.BaseCost,
                MarkupPercent = markup,
                PricingPolicyId = request.PricingPolicyId
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = product.Id }, product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateProductDto request)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();

            decimal markup = request.MarkupPercent ?? 0;

            if (request.PricingPolicyId.HasValue)
            {
                var policy = await _context.PricingPolicies.FindAsync(request.PricingPolicyId.Value);
                if (policy == null)
                    return BadRequest("Политика не найдена");

                markup = policy.DefaultMarkupPercent;
            }

            product.Name = request.Name;
            product.Description = request.Description;
            product.BaseCost = request.BaseCost;
            product.MarkupPercent = markup;
            product.PricingPolicyId = request.PricingPolicyId;

            await _context.SaveChangesAsync();
            return Ok(product);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }

}
