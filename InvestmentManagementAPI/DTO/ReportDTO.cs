using System.ComponentModel.DataAnnotations;

namespace InvestmentManagementAPI.DTO
{
    public class ReportDTO
    {
        [Required]
        public int ReportId { get; set; }
        [Required]
        public int PortfolioId { get; set; }
        [Required]
        public string ReportType { get; set; }
        [Required]
        public DateTime ReportDate { get; set; }
    }
}
