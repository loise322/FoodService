using System;
using System.Xml.Serialization;

namespace TravelLine.Food.Core.Import.Models
{
    [XmlRoot( ElementName = "Отпуск" )]
    public class VacationModel
    {
        [XmlAttribute( AttributeName = "Физлицо" )]
        public string Физлицо { get; set; }

        [XmlAttribute( AttributeName = "Организация" )]
        public string Организация { get; set; }

        [XmlAttribute( AttributeName = "Период" )]
        public string Period { get; set; }

        [XmlAttribute( AttributeName = "Дни" )]
        public int Days { get; set; }
    }
}
