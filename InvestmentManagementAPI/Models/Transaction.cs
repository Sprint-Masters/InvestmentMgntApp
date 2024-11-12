namespace InvestmentManagementAPI.Models
{
    public class Transaction
    {
        public int TransactionId {  get; set; }
        public int AssetId {  get; set; }
        public string TransactionType { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }

        public virtual Asset Asset { get; set; }

    }
}
