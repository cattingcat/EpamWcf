using BusinessLogic;
using DataAccessors.Accessors;
using DataAccessors.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace WCFServiceServer
{

    class Program
    {
        static void Main(string[] args)
        {
            //ServiceHost;
            IAccessor<Person> personAccessor = new OrmPersonAccessor("serviceDb");
            IAccessor<Phone> phoneAccessor = new OrmPhoneAccessor("serviceDb");

            ServiceImpl.PersonBusinessLogic = new PersonBll(personAccessor, phoneAccessor);
            ServiceImpl.PhoneBusinessLogic = new PhoneBll(personAccessor, phoneAccessor);

            ServiceHost host = new ServiceHost(typeof(ServiceImpl));
            host.Open();


            Console.WriteLine("Press X to Fin");
            Console.ReadKey();
            host.Close();
            Console.WriteLine("finished");
            Console.ReadKey();
        }
    }
}
