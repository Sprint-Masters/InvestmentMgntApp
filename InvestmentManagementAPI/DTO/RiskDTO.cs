using System.ComponentModel.DataAnnotations;

namespace InvestmentManagementAPI.DTO
{
    public class RiskDTO
    {
        [Required]
        public int RiskId { get; set; }
        [Required]
        public int PortfolioId { get; set; }
        [Required]
        public decimal RiskLevel { get; set; }
        [Required]
        public string RiskType { get; set; }
    }
}
