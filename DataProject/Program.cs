using DataAccessors.Accessors;
using DataAccessors.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInitializer
{
    class Program
    {
        static void Main(string[] args)
        {
            Random r = new Random();

            #region File init
            /*FilePersonAccessor fpa = new FilePersonAccessor(@"App_Data\FileDbs\FilePersonDb.xml");            
            for (int i = 0; i < 10; ++i)
            {
                fpa.Insert(new Person
                {
                    Id = i,
                    DayOfBirth = new DateTime(r.Next(50) + 1956, r.Next(11) + 1, r.Next(20) + 1),
                    Name = "Default name",
                    LastName = "Delault last name"
                });
            }

            FilePhoneAccessor fpha = new FilePhoneAccessor(@"App_Data\FileDbs\FilePhoneDb.xml");
            for (int i = 0; i < 10; ++i)
            {
                fpha.Insert(new Phone
                {
                    Id = i,
                    Number = Guid.NewGuid().ToString(),
                    PersonId = i
                });
            }*/
            #endregion

            #region Dir init
            DirectoryPersonAccessor fpa = new DirectoryPersonAccessor(@"App_Data\FolderDb\Persons");
            for (int i = 0; i < 10; ++i)
            {
                fpa.Insert(new Person
                {
                    Id = i,
                    DayOfBirth = new DateTime(r.Next(50) + 1956, r.Next(11) + 1, r.Next(20) + 1),
                    Name = "Default name",
                    LastName = "Delault last name"
                });
            }

            DirectoryPhoneAccessor fpha = new DirectoryPhoneAccessor(@"App_Data\FolderDb\Phones");
            for (int i = 0; i < 10; ++i)
            {
                fpha.Insert(new Phone
                {
                    Id = i,
                    Number = Guid.NewGuid().ToString(),
                    PersonId = i
                });
            }
            #endregion

            #region Db init
            /* ADOPersonAccessor fpa = new ADOPersonAccessor(@"ServiceDb");
            for (int i = 0; i < 10; ++i)
            {
                fpa.Insert(new Person
                {
                    Id = i,
                    DayOfBirth = new DateTime(r.Next(50) + 1956, r.Next(11) + 1, r.Next(20) + 1),
                    Name = "Default name",
                    LastName = "Delault last name"
                });
            }

            ADOPhoneAccessor fpha = new ADOPhoneAccessor(@"ServiceDb");
            for (int i = 0; i < 10; ++i)
            {
                fpha.Insert(new Phone
                {
                    Id = i,
                    Number = Guid.NewGuid().ToString(),
                    PersonId = i
                });
            } */
            #endregion
        }
    }
}
