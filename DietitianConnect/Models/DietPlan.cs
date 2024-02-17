using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DietitianConnect.Models
{
    public class DietPlan
    {
        [Key]
        public int PlanID { get; set; }
        public int UserID { get; set; }
        public int DietitianID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TargetWeight { get; set; }
        public string? DietDescription { get; set; }

        [ForeignKey("UserID")]
        public User? User { get; set; }

        [ForeignKey("DietitianID ")]
        public Dietitian? Dietitian { get; set; }

    }
}
