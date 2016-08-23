using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fadb_api.Models
{
    public class InMemoryFirearmRepository : IFirearmRepository
    {
        private static ConcurrentDictionary<string, Firearm> _firearms =
                new ConcurrentDictionary<string, Firearm>();

        public InMemoryFirearmRepository()
        {
            Add(new Firearm { Name = "M&P 15", SerialNumber = "1234d", IsNfaRegistered = false });
        }

        public void Add(Firearm firearm)
        {
            firearm.Key = Guid.NewGuid().ToString();
            _firearms[firearm.Key] = firearm;
        }

        public Firearm Find(string key)
        {
            Firearm firearm;
            _firearms.TryGetValue(key, out firearm);
            return firearm;
        }

        public IEnumerable<Firearm> GetAll()
        {
            return _firearms.Values;
        }

        public Firearm Remove(string key)
        {
            Firearm firearm;
            _firearms.TryRemove(key, out firearm);
            return firearm;
        }

        public void Update(Firearm firearm)
        {
            _firearms[firearm.Key] = firearm;
        }
    }
}
