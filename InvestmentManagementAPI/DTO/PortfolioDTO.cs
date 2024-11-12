using System.ComponentModel.DataAnnotations;

namespace InvestmentManagementAPI.DTO
{
    public class PortfolioDTO
    {
        [Required]
        public int PortfolioId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal TotalValue { get; set; }
    }
}
