namespace InvestmentManagementAPI.Models
{
    public class Report
    {
        public int ReportId {  get; set; }
        public int PortfolioId {  get; set; }
        public string ReportType {  get; set; }
        public DateTime ReportDate { get; set; }
        public virtual Portfolio Portfolio { get; set; }
    }
}
