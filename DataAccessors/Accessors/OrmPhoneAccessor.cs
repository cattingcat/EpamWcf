using System.Collections.Generic;
using System.Data.Common;
using System.Configuration;

using DataAccessors.Entity;

using MyOrm;

namespace DataAccessors.Accessors
{
    public class OrmPhoneAccessor: IAccessor<Phone>
    {
        private MyORM _orm;

        public OrmPhoneAccessor(string appConfigConnectionString)
        {
            _orm = new MyORM(appConfigConnectionString, typeof(Phone), typeof(Person));
            _orm.Lazy = false;
        }


        public IEnumerable<Phone> GetAll()
        {
            return _orm.SelectAll<Phone>();
        }

        public Phone GetById(object id)
        {
            return _orm.SelectById<Phone>(id);
        }

        public void DeleteById(object id)
        {
            _orm.Delete<Phone>(id);
        }

        public void Insert(Phone p)
        {
            _orm.Insert(p);
        }
    }
}
