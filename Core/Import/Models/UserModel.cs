using System.Xml.Serialization;

namespace TravelLine.Food.Core.Import.Models
{
    [XmlRoot( ElementName = "Физлицо" )]
    public class UserModel
    {
        [XmlAttribute( AttributeName = "ИдентификаторФизлица" )]
        public string Id { get; set; }

        [XmlAttribute( AttributeName = "Код" )]
        public string Code { get; set; }

        [XmlAttribute( AttributeName = "ФИО" )]
        public string FullName { get; set; }

        [XmlAttribute( AttributeName = "Фамилия" )]
        public string LastName { get; set; }

        [XmlAttribute( AttributeName = "Имя" )]
        public string Name { get; set; }

        [XmlAttribute( AttributeName = "Отчество" )]
        public string SecondName { get; set; }

        [XmlAttribute( AttributeName = "ИНН" )]
        public string INN { get; set; }
    }
}
