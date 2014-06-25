using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

using DataAccessors.Entity;

namespace DataAccessors.Accessors
{
    public class FilePhoneAccessor: IAccessor<Phone>
    {
       private static XmlSerializer PhoneArraySerializer = 
            new XmlSerializer(typeof(List<Phone>), new[] { typeof(Phone) });

        private ICollection<Phone> _data;
        private string _fileName;

        public FilePhoneAccessor(string fileName)
        {
            this._fileName = fileName;
            try
            {
                _data = DeserializeCollection();
            }
            catch
            {
                _data = new LinkedList<Phone>();
            }
        }

        public IEnumerable<Phone> GetAll()
        {
            return _data;
        }

        public Phone GetById(object id)
        {
            var res = from p in _data where p.Id == (int)id select p;
            return res.FirstOrDefault<Phone>();
        }

        public void DeleteById(object id)
        {
            var res = from p in _data where p.Id == (int)id select p;
            if (res.FirstOrDefault<Phone>() != null)
            {
                Phone existPhone = res.First<Phone>();
                _data.Remove(existPhone);
            }
            SerializeCollection(_data);
        }

        public void Insert(Phone p)
        {
            var tmp = from ep in _data where ep.Id == p.Id select ep;
            Phone existPhone = tmp.FirstOrDefault<Phone>();
            if (existPhone != null)
            {
                _data.Remove(existPhone);
            }
            _data.Add(p);
            SerializeCollection(_data);
        }


        #region helpers
        private void SerializeCollection(ICollection<Phone> collection)
        {
            using (StreamWriter sw = new StreamWriter(_fileName))
            {
                PhoneArraySerializer.Serialize(sw, collection.ToList<Phone>());
            }
        }

        private ICollection<Phone> DeserializeCollection()
        {
            using (StreamReader sr = new StreamReader(_fileName))
            {
                return (List<Phone>)PhoneArraySerializer.Deserialize(sr);
            }
        }
        #endregion
    }
}
