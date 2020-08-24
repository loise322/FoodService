using System;
using System.Collections.Generic;

namespace TravelLine.Food.WebApi.Models
{
    public class MenuDto
    {
        public DateTime Date { get; set; }
        public List<DishDto> Dishes { get; set; }
    }
}
