using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DietitianConnect.Models
{
    public class MealTracking
    {
        [Key]
        public int TrackingID { get; set; }
        public int UserID { get; set; }
        public DateTime PublicationDate { get; set; }
        public string? MealType { get; set; }
        public string? MealName { get; set; }
        public decimal Quantity { get; set; }
        public int Calories { get; set; }
        public decimal Protein { get; set; }
        public decimal Carbohydrates { get; set; }
        public decimal Fat { get; set; }

        [ForeignKey("UserID")]
        public User? User { get; set; }

    }
}
