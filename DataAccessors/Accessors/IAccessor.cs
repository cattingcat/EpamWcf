using System;
using System.Collections.Generic;

namespace DataAccessors.Accessors
{
    public interface IAccessor<T>
    {
        IEnumerable<T> GetAll();

        T GetById(object id);

        void DeleteById(object id);

        void Insert(T p);
    }
}
