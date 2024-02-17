using System.ComponentModel.DataAnnotations;

namespace DietitianConnect.Models
{
    public class Admin
    {
        [Key]
        public int AdminID { get; set; }
        public string? AdminName { get; set; }
        public string? PasswordHash { get; set; }
        public string? Email { get; set; }
        public string? AuthorizationLevel { get; set; }
    }
}
