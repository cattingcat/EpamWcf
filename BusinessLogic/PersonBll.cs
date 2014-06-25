using DataAccessors.Accessors;
using DataAccessors.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class PersonBll: IPersonBll
    {
        private IAccessor<Person> _personAccessor;
        private IAccessor<Phone> _phoneAccessor;

        public PersonBll(IAccessor<Person> personAccessor, IAccessor<Phone> phoneAccessor)
        {
            _personAccessor = personAccessor;
            _phoneAccessor = phoneAccessor;
        }

        public IEnumerable<Person> GetPersons()
        {
            var persons = _personAccessor.GetAll();
            var phones = _phoneAccessor.GetAll();
            foreach (var person in persons)
            {
                if (person.Phones == null)
                {
                    IEnumerable<Phone> joinedPhones = from phone in phones where phone.PersonId == person.Id select phone;

                    person.Phones = joinedPhones;
                }
            }
            return persons;
        }

        public Person GetPerson(object id)
        {
            Person person = _personAccessor.GetById(id);
            if (person != null)
            {
                var phones = _phoneAccessor.GetAll();
                var joinedPhones = from phone in phones where phone.PersonId == person.Id select phone;

                person.Phones = joinedPhones;
            }
            return person;
        }

        public void DeletePerson(object personId)
        {
            Person person = _personAccessor.GetById(personId);
            if (person.Phones != null)
            {
                foreach (var phone in person.Phones)
                    _phoneAccessor.DeleteById(phone.Id);
            }
            _personAccessor.DeleteById(person.Id);
        }

        public void UpdatePerson(Person person)
        {
            if (_personAccessor.GetById(person.Id) != null)
            {
                DeletePerson(person);
                AddPerson(person);
            }
        }

        public void AddPerson(Person person)
        {
            _personAccessor.Insert(person);
            if (person.Phones != null)
            {
                foreach (var phone in person.Phones)
                    _phoneAccessor.Insert(phone);
            }
        }
    }
}
