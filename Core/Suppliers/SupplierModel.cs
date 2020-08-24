namespace TravelLine.Food.Core.Suppliers
{
    public class SupplierModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string ContactPerson { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string LegalEntity { get; set; }

        public int Discount { get; set; }

        public int SalatWareCost { get; set; }

        public int SoupWareCost { get; set; }

        public int SecondWareCost { get; set; }
    }
}
