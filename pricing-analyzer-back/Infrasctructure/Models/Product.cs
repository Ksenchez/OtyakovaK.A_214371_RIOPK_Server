using System.ComponentModel.DataAnnotations;

namespace pricing_analyzer_back.Infrasctructure.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public decimal BaseCost { get; set; }
        public decimal MarkupPercent { get; set; }
        public decimal FinalPrice => BaseCost * (1 + MarkupPercent / 100);
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int? PricingPolicyId { get; set; }
        public PricingPolicy? PricingPolicy { get; set; }
    }

}
