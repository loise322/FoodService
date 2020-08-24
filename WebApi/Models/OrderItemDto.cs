using System;
using System.Collections.Generic;

namespace TravelLine.Food.WebApi.Models
{
    public class OrderItemDto
    {
        public List<DishDto> Dishes { get; set; }
    }
}
