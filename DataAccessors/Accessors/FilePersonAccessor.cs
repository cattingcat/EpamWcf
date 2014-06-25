using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using System.IO;

using DataAccessors.Entity;

namespace DataAccessors.Accessors
{
    public class FilePersonAccessor: IAccessor<Person>
    {
        private static XmlSerializer PersonArraySerializer = 
            new XmlSerializer(typeof(List<Person>), new[] { typeof(Person), typeof(Phone) });

        private ICollection<Person> _data;
        private string _fileName;

        public FilePersonAccessor(string fileName)
        {
            this._fileName = fileName;
            try
            {
                _data = DeserializeCollection();
            }
            catch
            {
                _data = new LinkedList<Person>();
            }
        }

        public IEnumerable<Person> GetAll()
        {
            return _data;
        }

        public Person GetById(object id)
        {
            var res = from p in _data where p.Id == (int)id select p;
            return res.FirstOrDefault<Person>();
        }

        public void DeleteById(object id)
        {
            var res = from p in _data where p.Id == (int)id select p;
            if (res.FirstOrDefault<Person>() != null)
            {
                Person existPerson = res.First<Person>();
                _data.Remove(existPerson);
            }
            SerializeCollection(_data);
        }

        public void Insert(Person p)
        {
            var tmp = from ep in _data where ep.Id == p.Id select ep;
            Person existPerson = tmp.FirstOrDefault<Person>();
            if (existPerson != null)
            {
                _data.Remove(existPerson);
            }            
            _data.Add(p);
            SerializeCollection(_data);
        }


        #region helpers
        private void SerializeCollection(ICollection<Person> collection)
        {
            using (StreamWriter sw = new StreamWriter(_fileName))
            {
                PersonArraySerializer.Serialize(sw, collection.ToList<Person>());
            }
        }

        private ICollection<Person> DeserializeCollection()
        {
            using (StreamReader sr = new StreamReader(_fileName))
            {               
                return (List<Person>)PersonArraySerializer.Deserialize(sr);
            }
        }
        #endregion
    }
}
