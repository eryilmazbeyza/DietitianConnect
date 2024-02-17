using System.ComponentModel.DataAnnotations;

namespace DietitianConnect.Models
{
    public class Dietitian
    {
        [Key]
        public int DietitianID { get; set; }
        public string? FullName { get; set; }
        public string? ExpertiseArea { get; set; }
        public string? ContactInformation { get; set; }

    }
}
