using System.ComponentModel.DataAnnotations;

namespace pricing_analyzer_back.Infrasctructure.Models
{
    public class PricingPolicy
    {
        [Key]
        public int Id { get; set; }
        public string PolicyName { get; set; } = default!;
        public string Description { get; set; } = default!;
        public decimal DefaultMarkupPercent { get; set; }
        public bool IsActive { get; set; } = true;
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }

}
