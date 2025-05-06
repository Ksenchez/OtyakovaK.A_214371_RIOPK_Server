using System.ComponentModel.DataAnnotations;

namespace pricing_analyzer_back.Infrasctructure.Models
{
    public class ProductCalculation
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; } = default!;
        public int UserId { get; set; }
        public User User { get; set; } = default!;
        public DateTime CalculatedAt { get; set; } = DateTime.UtcNow;
        public decimal CustomMarkup { get; set; } // Для собственной наценки
        public decimal CalculatedPrice => Product.BaseCost * (1 + CustomMarkup / 100);
    }
}
