using DataAccessors.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public interface IPersonBll
    {
        IEnumerable<Person> GetPersons();
        Person GetPerson(object id);
        void DeletePerson(object personId);
        void UpdatePerson(Person person);
        void AddPerson(Person person);
    }
}
