using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

using DataAccessors.Entity;

namespace DataAccessors.Accessors
{
    public class DirectoryPhoneAccessor: IAccessor<Phone>
    {
        private static XmlSerializer serializer = new XmlSerializer(typeof(Phone));

        private string _directoryName;

        public DirectoryPhoneAccessor(string path)
        {
            _directoryName = path;
        }

        public IEnumerable<Phone> GetAll()
        {
            ICollection<Phone> res = new List<Phone>();
            foreach (string filename in Directory.EnumerateFiles(_directoryName, "*.xml"))
            {
                using (FileStream fs = File.Open(filename, FileMode.Open))
                {
                    Phone p = (Phone)serializer.Deserialize(fs);
                    res.Add(p);
                }
            }
            return res;
        }

        public Phone GetById(object id)
        {
            string path = GetFileName((int)id);
            if (File.Exists(path))
            {
                using (FileStream fs = new FileStream(path, FileMode.Open))
                {
                    return (Phone)serializer.Deserialize(fs);
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

        public void Insert(Phone p)
        {
            CreateOrReplace(p);
        }


        #region helpers
        private void SerializeCollection(ICollection<Phone> collection)
        {
            foreach (Phone p in collection)
            {
                CreateOrIgnore(p);
            }
        }

        private void CreateOrReplace(Phone p)
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

        private void CreateOrIgnore(Phone p)
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
