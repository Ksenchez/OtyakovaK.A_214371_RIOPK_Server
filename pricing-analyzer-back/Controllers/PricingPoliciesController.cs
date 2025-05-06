using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pricing_analyzer_back.Infrasctructure.Context;
using pricing_analyzer_back.Infrasctructure.Models;
using System;

namespace pricing_analyzer_back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PricingPoliciesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PricingPoliciesController(ApplicationDbContext context) => _context = context;

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(await _context.PricingPolicies.ToListAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var policy = await _context.PricingPolicies.FindAsync(id);
            return policy == null ? NotFound() : Ok(policy);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PricingPolicy policy)
        {
            _context.PricingPolicies.Add(policy);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAll), new { id = policy.Id }, policy);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PricingPolicy input)
        {
            var policy = await _context.PricingPolicies.FindAsync(id);
            if (policy == null) return NotFound();

            policy.PolicyName = input.PolicyName;
            policy.Description = input.Description;
            policy.DefaultMarkupPercent = input.DefaultMarkupPercent;
            policy.IsActive = input.IsActive;

            await _context.SaveChangesAsync();
            return Ok(policy);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var policy = await _context.PricingPolicies.FindAsync(id);
            if (policy == null) return NotFound();

            _context.PricingPolicies.Remove(policy);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
