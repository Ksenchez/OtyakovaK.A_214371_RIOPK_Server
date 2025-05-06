namespace pricing_analyzer_back.Infrasctructure.Models.Dto
{
    public class CalculationDto
    {
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public decimal CustomMarkup { get; set; }
    }
}
