using BusinessLogic;
using DataAccessors.Accessors;
using DataAccessors.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WCFServiceServer
{
    [ServiceContract]
    public interface IServicePersonAccessor
    {
        [OperationContract]
        Person[] GetPersons();

        [OperationContract]
        Person GetPerson(int id);

        [OperationContract(IsOneWay=true)]
        void DeletePerson(int personId);

        [OperationContract(IsOneWay = true)]
        void UpdatePerson(Person person);

        [OperationContract(IsOneWay = true)]
        void AddPerson(Person person);
    }

    [ServiceContract]
    public interface IServicePhoneAccessor
    {
        [OperationContract]
        Phone[] GetPhones();

        [OperationContract(Name="GetPhonesByPersonId")]
        Phone[] GetPhones(int personId);

        [OperationContract]
        Phone GetPhone(int id);

        [OperationContract(IsOneWay = true)]
        void DeletePhone(int phoneId);

        [OperationContract(IsOneWay = true)]
        void UpdatePhone(Phone phone);

        [OperationContract(IsOneWay = true)]
        void AddPhone(Phone phone);
    }

    [ServiceBehavior(InstanceContextMode=InstanceContextMode.Single)]
    internal class ServiceImpl : IServicePersonAccessor, IServicePhoneAccessor
    {
        private IPersonBll _personBusinessLogic; 
        private IPhoneBll _phoneBusinessLogic;

        public ServiceImpl(IPersonBll personBll, IPhoneBll phoneBll)
        {
            _personBusinessLogic = personBll;
            _phoneBusinessLogic = phoneBll;
        }

        #region PersonBll
        public Person[] GetPersons()
        {
            return _personBusinessLogic.GetPersons().ToArray();
        }

        public Person GetPerson(int id)
        {
            return _personBusinessLogic.GetPerson(id);
        }

        public void DeletePerson(int personId)
        {
            _personBusinessLogic.DeletePerson(personId);
        }

        public void UpdatePerson(Person person)
        {
            _personBusinessLogic.UpdatePerson(person);
        }

        public void AddPerson(Person person)
        {
            _personBusinessLogic.AddPerson(person);
        }
        #endregion

        #region PhonesBll
        public Phone[] GetPhones()
        {
            return _phoneBusinessLogic.GetPhones().ToArray();
        }

        public Phone[] GetPhones(int personId)
        {
            return _phoneBusinessLogic.GetPhones(personId).ToArray();
        }

        public Phone GetPhone(int id)
        {
            return _phoneBusinessLogic.GetPhone(id);
        }

        public void DeletePhone(int phoneId)
        {
            _phoneBusinessLogic.DeletePhone(phoneId);
        }

        public void UpdatePhone(Phone phone)
        {
            _phoneBusinessLogic.UpdatePhone(phone);
        }

        public void AddPhone(Phone phone)
        {
            _phoneBusinessLogic.AddPhone(phone);
        }
        #endregion
    }

}
