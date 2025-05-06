namespace pricing_analyzer_back.Infrasctructure.Models.Dto
{
    public class CreateProductDto
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public decimal BaseCost { get; set; }
        public decimal? MarkupPercent { get; set; }
        public int? PricingPolicyId { get; set; }
    }

}
