using System;
using System.Data;

using MyOrm.Attributes;
using System.Xml.Serialization;

namespace DataAccessors.Entity
{
    [Serializable]
    [Table(TableName = "PhoneTbl")]
    public class Phone
    {
        [Id(ColumnName = "id", ColumnType = DbType.Int32)]
        public int Id { get; set; }
        [Column(ColumnName = "number", ColumnType = DbType.String)]
        public string Number { get; set; }
        [Column(ColumnName = "person_id", ColumnType = DbType.Int32)]
        public int PersonId { get; set; }
    }
}
