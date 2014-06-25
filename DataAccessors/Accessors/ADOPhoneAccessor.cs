using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;

using DataAccessors.Entity;

namespace DataAccessors.Accessors
{
    public class ADOPhoneAccessor: IAccessor<Phone>
    {
        private DbProviderFactory _factory;
        private string _connectionString;

        private static string PhoneDbFields = "id, number, person_id";
        private static string TblName = "PhoneTbl";

        public ADOPhoneAccessor(string appConfigConnectionString)
        {
            _connectionString = ConfigurationManager.ConnectionStrings[appConfigConnectionString].ConnectionString;
            string providerName = ConfigurationManager.ConnectionStrings[appConfigConnectionString].ProviderName;
            _factory = DbProviderFactories.GetFactory(providerName);
        }


        public IEnumerable<Phone> GetAll()
        {
            DbConnection conn = _factory.CreateConnection();
            conn.ConnectionString = _connectionString;
            conn.Open();
            using (conn)
            {
                DbCommand comm = conn.CreateCommand();
                comm.CommandText = "SELECT " + PhoneDbFields + " FROM " + TblName;
                DbDataReader reader = comm.ExecuteReader();
                List<Phone> result = new List<Phone>();
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string number = string.Empty;
                    int person_id = -1;

                    if(!reader.IsDBNull(1))
                        number = reader.GetString(1);

                    if(!reader.IsDBNull(2))
                        person_id = reader.GetInt32(2);

                    result.Add(new Phone { Id = id, Number = number, PersonId = person_id });
                }
                return result;
            }            
        }

        public Phone GetById(object id)
        {
            DbConnection conn = _factory.CreateConnection();
            conn.ConnectionString = _connectionString;
            conn.Open();
            using (conn)
            {
                DbCommand comm = conn.CreateCommand();
                comm.CommandText = 
                    "SELECT " + PhoneDbFields +
                    " FROM " + TblName + " WHERE id=@id";
                DbParameter param = comm.CreateParameter();
                param.DbType = System.Data.DbType.Int32; 
                param.ParameterName="@id";
                param.Value = id;          
                comm.Parameters.Add(param);
                
                DbDataReader reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    int _id = reader.GetInt32(0);
                    string number = string.Empty;
                    int person_id = -1;

                    if (!reader.IsDBNull(1))
                        number = reader.GetString(1);

                    if (!reader.IsDBNull(2))
                        person_id = reader.GetInt32(2);

                    return new Phone { Id = _id, Number = number, PersonId = person_id };
                }
            }
            return null;
        }

        public void DeleteById(object id)
        {
            DbConnection conn = _factory.CreateConnection();
            conn.ConnectionString = _connectionString;
            conn.Open();
            using (conn)
            {
                DbCommand comm = conn.CreateCommand();
                comm.CommandText =
                    "DELETE FROM " + TblName + " WHERE id=@id";
                DbParameter param = comm.CreateParameter();
                param.DbType = System.Data.DbType.Int32;
                param.ParameterName = "@id";
                param.Value = id;
                comm.Parameters.Add(param);
                comm.ExecuteNonQuery();
            }
        }

        public void Insert(Phone p)
        {
            DbConnection conn = _factory.CreateConnection();
            conn.ConnectionString = _connectionString;
            conn.Open();
            using (conn)
            {
                DbCommand comm = conn.CreateCommand();
                comm.CommandText = 
                    String.Format("INSERT INTO {0}({1}) VALUES(@id, @number, @personid)", TblName, PhoneDbFields);
                DbParameter param = comm.CreateParameter();
                param.DbType = System.Data.DbType.Int32;
                param.ParameterName = "@id";
                param.Value = p.Id;
                comm.Parameters.Add(param);

                param = comm.CreateParameter();
                param.DbType = System.Data.DbType.String;
                param.ParameterName = "@number";
                param.Value = p.Number;
                comm.Parameters.Add(param);

                param = comm.CreateParameter();
                param.DbType = System.Data.DbType.Int32;
                param.ParameterName = "@personid";
                param.Value = p.PersonId;
                comm.Parameters.Add(param);

                comm.ExecuteNonQuery();
            }
        }
    }
}
