using System.Collections.Generic;
using System.Linq;
using System;

using DataAccessors.Entity;

namespace DataAccessors.Accessors
{
    public class MemoryPersonAccessor: IAccessor<Person>
    {
        private ICollection<Person> _data;
       

        public MemoryPersonAccessor(ICollection<Person> data)
        {
            this._data = data;
        }

        public MemoryPersonAccessor()
        {
            LinkedList<Person> tmp = new LinkedList<Person>();
            Random rand = new Random();
            for (int i = 0; i < 10; ++i)
            {
                tmp.AddFirst(new Person
                {
                    Id = i,
                    LastName = String.Format("{0} lastname", i.ToString()),
                    Name = String.Format("{0} name", i.ToString()),
                    DayOfBirth = DateTime.Today
                });
            }
            this._data = tmp;
        }


        public IEnumerable<Person> GetAll()
        {
            return _data;
        }

        public Person GetById(object id)
        {
            var res = from p in _data where p.Id == (int)id select p;
            return res.FirstOrDefault<Person>();
        }

        public void DeleteById(object id)
        {
            var res = from p in _data where p.Id == (int)id select p;
            Person exPerson = res.FirstOrDefault<Person>();
            if (exPerson != null)
            {
                _data.Remove(exPerson);
            }
        }

        public void Insert(Person p)
        {
            var tmp = from ep in _data where ep.Id == p.Id select ep;
            Person existPerson = tmp.FirstOrDefault<Person>();
            if (existPerson != null)
            {
                _data.Remove(existPerson);
            }
            _data.Add(p);            
        }
    }
}
