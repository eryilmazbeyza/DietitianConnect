using System.ComponentModel.DataAnnotations;

namespace DietitianConnect.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }
        public string? UserName { get; set; }
        public string? PasswordHash { get; set; }
        public string? Email { get; set; }
        public DateTime BirthDate { get; set; }
        public string? Gender { get; set; }
        public string? ProfilePicturePath { get; set; }

    }
}
