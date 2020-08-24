using System.Xml.Serialization;

namespace TravelLine.Food.Core.Import.Models
{
    [XmlRoot( ElementName = "Операции" )]
    public class ImportModel
    {
        [XmlElement( ElementName = "Операция" )]
        public OperationModel Operation { get; set; }
    }
}
