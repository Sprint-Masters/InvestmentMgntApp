namespace InvestmentManagementAPI.DTO
{
    public class TransactionDTO
    {
        public int TransactionId { get; set; }
        public int AssetId { get; set; }
        public string TransactionType { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }

    }
}
