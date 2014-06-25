using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;

using DataAccessors.Entity;

namespace DataAccessors.Accessors
{
    public class ADOPersonAccessor: IAccessor<Person>
    {
        private DbProviderFactory _factory;
        private string _connectionString;    

        public ADOPersonAccessor(string appConfigConnectionString)
        {
            _connectionString = ConfigurationManager.ConnectionStrings[appConfigConnectionString].ConnectionString;
            string providerName = ConfigurationManager.ConnectionStrings[appConfigConnectionString].ProviderName;
            _factory = DbProviderFactories.GetFactory(providerName);
        }


        public IEnumerable<Person> GetAll()
        {
            DbConnection conn = _factory.CreateConnection();
            conn.ConnectionString = _connectionString;
            conn.Open();
            using (conn)
            {
                DbCommand comm = conn.CreateCommand();
                comm.CommandText = "SELECT id, name, lastname, dob from PersonTbl";
                DbDataReader reader = comm.ExecuteReader();
                List<Person> result = new List<Person>();
                while (reader.Read())
                {
                    Person p = ProcessDataReader(reader);
                    result.Add(p);
                }
                return result;
            }            
        }

        public Person GetById(object id)
        {
            DbConnection conn = _factory.CreateConnection();
            conn.ConnectionString = _connectionString;
            conn.Open();
            using (conn)
            {
                DbCommand comm = conn.CreateCommand();
                comm.CommandText = 
                    "SELECT id, name, lastname"+
                    " FROM PersonTbl WHERE id=@id";
                DbParameter param = comm.CreateParameter();
                param.DbType = System.Data.DbType.Int32; 
                param.ParameterName="@id";
                param.Value = id;          
                comm.Parameters.Add(param);
                
                DbDataReader reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    return ProcessDataReader(reader);
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
                    "DELETE FROM PersonTbl WHERE id=@id";
                DbParameter param = comm.CreateParameter();
                param.DbType = System.Data.DbType.Int32;
                param.ParameterName = "@id";
                param.Value = id;
                comm.Parameters.Add(param);
                comm.ExecuteNonQuery();
            }
        }

        public void Insert(Person p)
        {
            DbConnection conn = _factory.CreateConnection();
            conn.ConnectionString = _connectionString;
            conn.Open();
            using (conn)
            {
                DbCommand comm = conn.CreateCommand();
                comm.CommandText =
                    "INSERT INTO PersonTbl(id, name, lastname, dob)"+
                    " VALUES(@id, @name, @lastName, @dob)";
                DbParameter param = comm.CreateParameter();
                param.DbType = System.Data.DbType.Int32;
                param.ParameterName = "@id";
                param.Value = p.Id;
                comm.Parameters.Add(param);

                param = comm.CreateParameter();
                param.DbType = System.Data.DbType.String;
                param.ParameterName = "@name";
                param.Value = p.Name;
                comm.Parameters.Add(param);

                param = comm.CreateParameter();
                param.DbType = System.Data.DbType.String;
                param.ParameterName = "@lastName";
                param.Value = p.LastName;
                comm.Parameters.Add(param);

                param = comm.CreateParameter();
                param.DbType = System.Data.DbType.Date;
                param.ParameterName = "@dob";
                param.Value = p.DayOfBirth;
                comm.Parameters.Add(param);

                comm.ExecuteNonQuery();
            }
        }


        private Person ProcessDataReader(DbDataReader reader)
        {
            int id = reader.GetInt32(0);
            string name = string.Empty;
            string lastName = string.Empty;
            DateTime dob = new DateTime();

            if (!reader.IsDBNull(1))
                name = reader.GetString(1);

            if (!reader.IsDBNull(2))
                lastName = reader.GetString(2);

            if (!reader.IsDBNull(3))
                dob = reader.GetDateTime(3);

            return new Person { Id = id, Name = name.Trim(), LastName = lastName.Trim(), DayOfBirth = dob };
        }
    }
}
