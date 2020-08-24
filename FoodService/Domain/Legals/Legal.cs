namespace TravelLine.Food.Domain.Legals
{
    /// <summary>
    /// Команда (Юр. лицо)
    /// </summary>
    public class Legal
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string FullName { get; set; }

        public string Code { get; set; }

        public string ExternalId { get; set; }

        public bool IsDeleted { get; set; }
    }
}
