using System;
using System.Collections.Generic;

namespace TravelLine.Food.WebApi.Models
{
    public class OrderDto
    {
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public List<OrderItemDto> Items { get; set; }
    }
}
