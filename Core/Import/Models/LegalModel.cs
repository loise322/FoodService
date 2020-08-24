using System.Xml.Serialization;

namespace TravelLine.Food.Core.Import.Models
{
    [XmlRoot( ElementName = "Организация" )]
    public class LegalModel
    {
        [XmlAttribute( AttributeName = "ИдентификаторОрганизации" )]
        public string Id { get; set; }

        [XmlAttribute( AttributeName = "Наименование" )]
        public string Name { get; set; }

        [XmlAttribute( AttributeName = "ИНН" )]
        public string INN { get; set; }
    }
}
