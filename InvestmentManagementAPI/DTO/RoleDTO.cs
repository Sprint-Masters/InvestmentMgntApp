using System.ComponentModel.DataAnnotations;

namespace InvestmentManagementAPI.DTO
{
    public class RoleDTO
    {
        [Required]
        public int RoleId { get; set; }
        [Required]
        public string RoleName { get; set; }
    }
}
