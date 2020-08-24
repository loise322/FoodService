using System;
using System.Xml.Serialization;

namespace TravelLine.Food.Core.Import.Models
{
    [XmlRoot( ElementName = "Явка" )]
    public class WorkDaysModel
    {
        [XmlAttribute( AttributeName = "Физлицо" )]
        public string UserId { get; set; }

        [XmlAttribute( AttributeName = "Организация" )]
        public string LegalId { get; set; }

        [XmlAttribute( AttributeName = "Период" )]
        public string Period { get; set; }

        [XmlAttribute( AttributeName = "Дни" )]
        public int Days { get; set; }

        [XmlAttribute( AttributeName = "ДатаПриема" )]
        public string StartDate { get; set; }

        [XmlAttribute( AttributeName = "ДатаУвольнения" )]
        public string EndDate { get; set; }
    }
}
