using System.Data.Entity;
using TravelLine.Food.Domain.Configs;
using TravelLine.Food.Domain.DeliveryOffices;
using TravelLine.Food.Domain.Dishes;
using TravelLine.Food.Domain.DishQuotas;
using TravelLine.Food.Domain.DishRatings;
using TravelLine.Food.Domain.Legals;
using TravelLine.Food.Domain.Menus;
using TravelLine.Food.Domain.Orders;
using TravelLine.Food.Domain.Suppliers;
using TravelLine.Food.Domain.Users;
using TravelLine.Food.Domain.WorkTimes;
using TravelLine.Food.Infrastructure.Mappings;

namespace TravelLine.Food.Infrastructure
{
    public class FoodContext : DbContext
    {
        public FoodContext( string connectionString ) : base( connectionString )
        {
        }

        public DbSet<Config> Configs { get; set; }
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<DishRating> DishRatings { get; set; }
        public DbSet<DishQuota> DishQuota { get; set; }
        public DbSet<DeliveryOffice> DeliveryOffices { get; set; }
        public DbSet<Legal> Legals { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserLegal> UserLegals { get; set; }
        public DbSet<WorkTime> WorkTimes { get; set; }

        protected override void OnModelCreating( DbModelBuilder modelBuilder )
        {
            modelBuilder.Configurations.Add( new ConfigConfiguration() );
            modelBuilder.Configurations.Add( new DishConfiguration() );
            modelBuilder.Configurations.Add( new DishRatingConfiguration() );
            modelBuilder.Configurations.Add( new DishQuotaConfiguration() );
            modelBuilder.Configurations.Add( new DeliveryOfficeConfiguration() );
            modelBuilder.Configurations.Add( new LegalConfiguration() );
            modelBuilder.Configurations.Add( new MenuConfiguration() );
            modelBuilder.Configurations.Add( new OrderConfiguration() );
            modelBuilder.Configurations.Add( new SupplierConfiguration() );
            modelBuilder.Configurations.Add( new UserConfiguration() );
            modelBuilder.Configurations.Add( new UserLegalConfiguration() );
            modelBuilder.Configurations.Add( new WorkTimeConfiguration() );
        }
    }
}
