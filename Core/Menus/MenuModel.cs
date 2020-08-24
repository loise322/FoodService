using System;
using System.Collections.Generic;
using System.Linq;
using TravelLine.Food.Core.Dishes;
using TravelLine.Food.Core.Users;
using TravelLine.Food.Domain.Dishes;

namespace TravelLine.Food.Core.Menus
{
    public class MenuModel
    {
        public MenuModel()
        {
            Dishes = new List<DishModel>();
        }

        public int Id { get; set; }

        public bool IsOrdered { get; set; }

        public DateTime Date { get; set; }

        public List<DishModel> Dishes { get; set; }

        public UserModel User { get; set; }

        public List<DishModel> GetByDishType( DishType type )
        {
            return Dishes.FindAll( x => x.Type == type );
        }

        public override string ToString()
        {
            if ( Dishes == null || Dishes.Count == 0 )
            {
                return String.Empty;
            }

            return String.Join( ",", Dishes.Select( d => d.Id ) );
        }
    }
}
