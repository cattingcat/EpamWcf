using DataAccessors.Accessors;
using DataAccessors.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class PhoneBll: IPhoneBll
    {
        private IAccessor<Person> _personAccessor;
        private IAccessor<Phone> _phoneAccessor;

        public PhoneBll(IAccessor<Person> personAccessor, IAccessor<Phone> phoneAccessor)
        {
            _personAccessor = personAccessor;
            _phoneAccessor = phoneAccessor;
        }

        public IEnumerable<Phone> GetPhones()
        {
            IPersonBll pbl = new PersonBll(_personAccessor, _phoneAccessor);

            var phones = _phoneAccessor.GetAll();
            return phones;
        }

        public IEnumerable<Phone> GetPhones(object personId)
        {
            return from p in GetPhones() where p.PersonId == (int)personId select p;
        }

        public Phone GetPhone(object id)
        {
            Phone phone = _phoneAccessor.GetById(id);
            IPersonBll pbl = new PersonBll(_personAccessor, _phoneAccessor);

            return phone;
        }

        public void DeletePhone(object phoneId)
        {
            _phoneAccessor.DeleteById(phoneId);
        }

        public void UpdatePhone(Phone phone)
        {
            if (_phoneAccessor.GetById(phone.Id) != null)
                DeletePhone(phone.Id);
            _phoneAccessor.Insert(phone);
        }

        public void AddPhone(Phone phone)
        {
            _phoneAccessor.Insert(phone);
        }
    }
}
