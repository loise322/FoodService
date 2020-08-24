using System;

namespace TravelLine.Food.Domain.Menus
{
    public class Menu
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public string Value { get; set; }

        public bool IsOrdered { get; set; }
    }
}
