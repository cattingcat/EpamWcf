using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

using DataAccessors.Entity;

namespace DataAccessors.Accessors
{
    public class DirectoryPersonAccessor: IAccessor<Person>
    {
        private static XmlSerializer serializer = new XmlSerializer(typeof(Person));

        private string _directoryName;

        public DirectoryPersonAccessor(string path)
        {
            _directoryName = path;
        }

        public IEnumerable<Person> GetAll()
        {
            ICollection<Person> res = new List<Person>();
            foreach (string filename in Directory.EnumerateFiles(_directoryName, "*.xml"))
            {
                using (FileStream fs = File.Open(filename, FileMode.Open))
                {
                    Person p = (Person)serializer.Deserialize(fs);
                    res.Add(p);
                }
            }
            return res;
        }

        public Person GetById(object id)
        {
            string path = GetFileName((int)id);
            if (File.Exists(path))
            {
                using (FileStream fs = new FileStream(path, FileMode.Open))
                {
                    return (Person)serializer.Deserialize(fs);
                }
            }
            else
            {
                return null;
            }
        }

        public void DeleteById(object id)
        {
            string path = GetFileName((int)id);
            File.Delete(path);
        }

        public void Insert(Person p)
        {
            CreateOrReplace(p);
        }


        #region helpers
        private void SerializeCollection(ICollection<Person> collection)
        {
            foreach (Person p in collection)
            {
                CreateOrIgnore(p);
            }
        }

        private void CreateOrReplace(Person p)
        {
            string path = GetFileName(p.Id);
            if (File.Exists(path))
            {
                using (FileStream fs = new FileStream(path, FileMode.Create))
                {
                    serializer.Serialize(fs, p);
                }
            }
            else
            {
                using (FileStream fs = new FileStream(path, FileMode.CreateNew))
                {
                    serializer.Serialize(fs, p);
                }
            }
        }

        private void CreateOrIgnore(Person p)
        {
            string path = GetFileName(p.Id);
            if (File.Exists(path))
            {
                return;
            }
            else
            {
                using (FileStream fs = new FileStream(path, FileMode.CreateNew))
                {
                    serializer.Serialize(fs, p);
                }
            }
        }

        private string GetFileName(int id)
        {
            string path = Path.Combine(_directoryName, id.ToString());
            return Path.ChangeExtension(path, "xml");
        }
        #endregion
    }
}
