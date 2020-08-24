using TravelLine.Food.Domain.Suppliers;

namespace TravelLine.Food.Domain.Dishes
{
    public class Dish
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DishType Type { get; set; }

        public decimal Cost { get; set; }

        public string ImagePath { get; set; }

        public bool IsSingle { get; set; }

        public int Weight { get; set; }

        public int SupplierId { get; set; }

        public virtual Supplier Supplier { get; set; }
    }
}
