using Microsoft.EntityFrameworkCore;

namespace DietitianConnect.Models
{
    public class DietitianContext:DbContext
    {
        public DietitianContext(DbContextOptions<DietitianContext>options) : base(options)  

        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Dietitian> Dietitians { get; set; }
        public DbSet<DietPlan> DietPlans { get; set; }
        public DbSet<MealTracking> MealTrackings { get; set; }
    }
}
