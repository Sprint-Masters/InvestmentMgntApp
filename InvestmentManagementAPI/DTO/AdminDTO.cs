using System.ComponentModel.DataAnnotations;

namespace InvestmentManagementAPI.DTO
{
    public class AdminDTO
    {
        [Required]
        public int AdminId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string AdminEmail { get; set; }
        [Required]
        public string AdminPassword { get; set; }
    }
}
