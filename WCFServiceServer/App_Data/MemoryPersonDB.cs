using DataAccessors.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessors.Data
{
    public static class MemoryDb
    {
        public static ICollection<Person> PersonData;
        public static ICollection<Phone> PhoneData;

        static MemoryDb()
        {
            LinkedList<Person> persons = new LinkedList<Person>();
            Random rand = new Random();
            for (int i = 0; i < 10; ++i)
            {
                persons.AddFirst(new Person { 
                    Id = i, 
                    LastName = String.Format("{0} lastname", i.ToString()), 
                    Name = String.Format("{0} name", i.ToString()), 
                    DayOfBirth = DateTime.Today });
            }
            PersonData = persons;

            LinkedList<Phone> phones = new LinkedList<Phone>();
            for (int i = 0; i < 10; ++i)
            {
                phones.AddFirst(new Phone
                {
                    Id = i,
                    Number = Guid.NewGuid().ToString(),
                    PersonId = i
                });
            }
            PhoneData = phones;
        }
    }
}
