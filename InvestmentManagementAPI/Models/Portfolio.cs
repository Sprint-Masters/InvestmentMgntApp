namespace InvestmentManagementAPI.Models
{
    public class Portfolio
    {
        public int PortfolioId { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal TotalValue { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Asset> Assets { get; set; } 
       
    }
}
