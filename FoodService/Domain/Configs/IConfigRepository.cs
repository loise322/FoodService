using System.Collections.Generic;

namespace TravelLine.Food.Domain.Configs
{
    public interface IConfigRepository
    {
        List<Config> GetAll();
    }
}
