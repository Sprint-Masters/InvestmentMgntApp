using System.ComponentModel.DataAnnotations;

namespace InvestmentManagementAPI.DTO
{
    public class AssetDTO
    {
        [Required] 
        public int AssetId { get; set; }
        [Required]
        public int PortfolioId { get; set; }
        [Required]
        public string AssetType { get; set; }
        [Required]
        public string Symbol { get; set; }
        [Required]
        public decimal Quantity { get; set; }
        [Required]
        public decimal CurrentPrice { get; set; }
        [Required]
        public decimal Value { get; set; }
    }
}
