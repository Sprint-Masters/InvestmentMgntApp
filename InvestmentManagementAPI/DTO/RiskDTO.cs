namespace InvestmentManagementAPI.DTO
{
    public class RiskDTO
    {
        public int RiskId { get; set; }
        public int PortfolioId { get; set; }
        public decimal RiskLevel { get; set; }
        public string RiskType { get; set; }
    }
}
