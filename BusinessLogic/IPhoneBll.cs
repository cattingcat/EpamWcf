using DataAccessors.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public interface IPhoneBll
    {
        IEnumerable<Phone> GetPhones();
        IEnumerable<Phone> GetPhones(object personId);
        Phone GetPhone(object id);
        void DeletePhone(object phoneId);
        void UpdatePhone(Phone phone);
        void AddPhone(Phone phone);
    }
}
