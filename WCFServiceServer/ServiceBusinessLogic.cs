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


    internal class ServiceImpl : IServicePersonAccessor, IServicePhoneAccessor
    {
        public static IPersonBll PersonBusinessLogic { get; set; }
        public static IPhoneBll PhoneBusinessLogic { get; set; }

        #region PersonBll
        public Person[] GetPersons()
        {
            return PersonBusinessLogic.GetPersons().ToArray();
        }

        public Person GetPerson(int id)
        {
            return PersonBusinessLogic.GetPerson(id);
        }

        public void DeletePerson(int personId)
        {
            PersonBusinessLogic.DeletePerson(personId);
        }

        public void UpdatePerson(Person person)
        {
            PersonBusinessLogic.UpdatePerson(person);
        }

        public void AddPerson(Person person)
        {
            PersonBusinessLogic.AddPerson(person);
        }
        #endregion

        #region PhonesBll
        public Phone[] GetPhones()
        {
            return PhoneBusinessLogic.GetPhones().ToArray();
        }

        public Phone[] GetPhones(int personId)
        {
            return PhoneBusinessLogic.GetPhones(personId).ToArray();
        }

        public Phone GetPhone(int id)
        {
            return PhoneBusinessLogic.GetPhone(id);
        }

        public void DeletePhone(int phoneId)
        {
            PhoneBusinessLogic.DeletePhone(phoneId);
        }

        public void UpdatePhone(Phone phone)
        {
            PhoneBusinessLogic.UpdatePhone(phone);
        }

        public void AddPhone(Phone phone)
        {
            PhoneBusinessLogic.AddPhone(phone);
        }
        #endregion
    }

}
