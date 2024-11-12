namespace InvestmentManagementAPI.DTO
{
    public class AssetDTO
    {
        public int AssetId { get; set; }
        public int PortfolioId { get; set; }
        public string AssetType { get; set; }
        public string Symbol { get; set; }
        public decimal Quantity { get; set; }
        public decimal CurrentPrice { get; set; }
        public decimal Value { get; set; }
    }
}
