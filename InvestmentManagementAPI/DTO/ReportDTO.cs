namespace InvestmentManagementAPI.DTO
{
    public class ReportDTO
    {
        public int ReportId { get; set; }
        public int PortfolioId { get; set; }
        public string ReportType { get; set; }
        public DateTime ReportDate { get; set; }
    }
}
