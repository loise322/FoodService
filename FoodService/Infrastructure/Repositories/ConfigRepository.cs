using System.Data.Entity;
using TravelLine.Food.Domain.Configs;
using TravelLine.Food.Infrastructure.Common;

namespace TravelLine.Food.Infrastructure.Repositories
{
    public class ConfigRepository : EFGenericRepository<Config>, IConfigRepository
    {
        public ConfigRepository( DbContext context ) : base( context ) { }
    }
}
