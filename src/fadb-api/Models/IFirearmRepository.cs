using System.Collections.Generic;

namespace fadb_api.Models
{
    public interface IFirearmRepository
    {
        void Add(Firearm firearm);
        IEnumerable<Firearm> GetAll();
        Firearm Find(string key);
        Firearm Remove(string key);
        void Update(Firearm firearm);
    }
}
