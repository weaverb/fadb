using System.Collections.Generic;

namespace fadb_api.Models
{
    public interface IFirearmRepository
    {
        void Add(Firearm firearm);
        IEnumerable<Firearm> GetAll();
        Firearm Find(int id);
        Firearm Remove(int id);
        void Update(Firearm firearm);
    }
}
