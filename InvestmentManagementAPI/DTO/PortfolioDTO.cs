namespace InvestmentManagementAPI.DTO
{
    public class PortfolioDTO
    {
        public int PortfolioId { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal TotalValue { get; set; }
    }
}
