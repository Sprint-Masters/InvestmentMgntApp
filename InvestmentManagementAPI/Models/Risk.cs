namespace InvestmentManagementAPI.Models
{
    public class Risk
    {
        public int RiskId { get; set; }
        public int PortfolioId {  get; set; }
        public decimal RiskLevel { get; set; }
        public string RiskType { get; set; }
        public virtual Portfolio Portfolio { get; set; }
    }
}
