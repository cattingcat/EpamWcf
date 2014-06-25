using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Configuration;

using DataAccessors.Entity;

using MyOrm;

namespace DataAccessors.Accessors
{
    public class OrmPersonAccessor: IAccessor<Person>
    {
        private MyORM _orm;

        public OrmPersonAccessor(string appConfigConnectionString)
        {
            _orm = new MyORM(appConfigConnectionString, typeof(Person), typeof(Phone));
            _orm.Lazy = false;
        }


        public IEnumerable<Person> GetAll()
        {
            return _orm.SelectAll<Person>();
        }

        public Person GetById(object id)
        {
            return _orm.SelectById<Person>(id);
        }

        public void DeleteById(object id)
        {
            int res = _orm.Delete<Person>(id);
        }

        public void Insert(Person p)
        {            
            int res = _orm.Insert(p);
            if (res <= 0)
            {
                throw new Exception("error: " + res);
            }
        }
    }
}
