using Microsoft.EntityFrameworkCore;
namespace InvestmentManagementAPI.Models
{
    public partial class InvestmentDBContext:DbContext
    {
        public InvestmentDBContext()
        {
        }
        public InvestmentDBContext(DbContextOptions<InvestmentDBContext> options) : base(options)
        {
        }

        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Portfolio> Portfolios { get; set; }
        public virtual DbSet<Asset> Assets { get; set; }
        public virtual DbSet<Risk> Risks { get; set; }
        public virtual DbSet<Report> Reports { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
       => optionsBuilder.UseSqlServer("Data Source=.\\sqlexpress;Initial Catalog=InvestmentDBContext;Integrated Security=True;TrustServerCertificate=True");

    }
}
