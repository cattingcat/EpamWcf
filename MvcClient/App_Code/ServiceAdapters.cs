using BusinessLogic;
using MvcClient.BusinessLogicService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcClient.App_Code
{
    public class PersonBusinessLogicServiceAdapter: IPersonBll
    {
        private IServicePersonAccessor _acc = new ServicePersonAccessorClient();

        public IEnumerable<DataAccessors.Entity.Person> GetPersons()
        {
            return _acc.GetPersons();
        }

        public DataAccessors.Entity.Person GetPerson(object id)
        {
            return _acc.GetPerson((int)id);
        }

        public void DeletePerson(object personId)
        {
            _acc.DeletePerson((int)personId);
        }

        public void UpdatePerson(DataAccessors.Entity.Person person)
        {
            _acc.UpdatePerson(person);
        }

        public void AddPerson(DataAccessors.Entity.Person person)
        {
            _acc.AddPerson(person);
        }
    }

    public class PhoneBusinessLogicServiceAdapter : IPhoneBll
    {
        private IServicePhoneAccessor _acc = new ServicePhoneAccessorClient();

        public IEnumerable<DataAccessors.Entity.Phone> GetPhones()
        {
            return _acc.GetPhones();
        }

        public IEnumerable<DataAccessors.Entity.Phone> GetPhones(object personId)
        {
            return _acc.GetPhonesByPersonId((int)personId);
        }

        public DataAccessors.Entity.Phone GetPhone(object id)
        {
            return _acc.GetPhone((int)id);
        }

        public void DeletePhone(object phoneId)
        {
            _acc.DeletePhone((int)phoneId);
        }

        public void UpdatePhone(DataAccessors.Entity.Phone phone)
        {
            _acc.UpdatePhone(phone);
        }

        public void AddPhone(DataAccessors.Entity.Phone phone)
        {
            _acc.AddPhone(phone);
        }
    }
}