using System;
using System.Data.Entity;
using System.Linq;
using TravelLine.Food.Domain.Menus;
using TravelLine.Food.Infrastructure.Common;

namespace TravelLine.Food.Infrastructure.Repositories
{
    public class MenuRepository : EFGenericRepository<Menu>, IMenuRepository
    {
        public MenuRepository( DbContext context ) : base( context ) { }

        public Menu Get( DateTime date )
        {
            return Query().FirstOrDefault( m => m.Date == date );
        }
        public Menu GetLastMenu()
        {
            return Query().OrderByDescending( m => m.Date ).FirstOrDefault();
        }
    }
}
