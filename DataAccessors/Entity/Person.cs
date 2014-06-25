using System;
using System.Collections.Generic;
using System.Data;
using System.Xml.Serialization;

using MyOrm.Attributes;

namespace DataAccessors.Entity
{
    [Serializable]
    [Table(TableName = "PersonTbl")]
    public class Person
    {
        public Person()
        {
            DayOfBirth = DateTime.Now;
        }

        [Id(ColumnName = "id", ColumnType = DbType.Int32)]
        public int Id { get; set; }
        [Column(ColumnName = "name", ColumnType = DbType.String)]
        public string Name { get; set; }
        [Column(ColumnName = "lastname", ColumnType = DbType.String)]
        public string LastName { get; set; }
        [Column(ColumnName = "dob", ColumnType = DbType.DateTime)]
        public DateTime DayOfBirth { get; set; }       
        [XmlIgnore]
        [Many(SecondTable = "PhoneTbl", SecondColumn = "person_id")]
        public IEnumerable<Phone> Phones { get; set; }
    }
}
