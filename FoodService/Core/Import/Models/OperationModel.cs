using System;
using System.Xml.Serialization;

namespace TravelLine.Food.Core.Import.Models
{
    [Serializable]
    public class OperationModel
    {
        [XmlAttribute( AttributeName = "Идентификатор" )]
        public string Id { get; set; }

        [XmlAttribute( AttributeName = "Тип" )]
        public string Type { get; set; }

        [XmlAttribute( AttributeName = "ДатаВыгрузки" )]
        public DateTime Date { get; set; }

        [XmlArray( ElementName = "Организации" )]
        [XmlArrayItem( "Организация" )]
        public LegalModel[] Legals { get; set; }

        [XmlArray( ElementName = "Физлица" )]
        [XmlArrayItem( "Физлицо" )]
        public UserModel[] Users { get; set; }

        [XmlArray( ElementName = "ОтработанноеВремя" )]
        [XmlArrayItem( "Явка" )]
        public WorkDaysModel[] WorkDays { get; set; }

        [XmlArray( ElementName = "Отпуска" )]
        [XmlArrayItem( "Отпуск" )]
        public VacationModel[] Vacations { get; set; }
    }
}
