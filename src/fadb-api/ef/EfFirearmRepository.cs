using fadb_api.Models;
using System.Collections.Generic;
using System.Linq;

namespace fadb_api.ef
{
    public class EfFirearmRepository : IFirearmRepository
    {
        private FirearmDbContext _context;
        public EfFirearmRepository(FirearmDbContext context)
        {
            _context = context;
         }

        public void Add(Firearm firearm)
        {
            _context.Firearms.Add(firearm);
            _context.SaveChanges();
        }

        public Firearm Find(int id)
        {
           return _context.Firearms.SingleOrDefault(x => x.Id == id);
        }

        public IEnumerable<Firearm> GetAll()
        {
            return _context.Firearms.ToList();
        }

        public Firearm Remove(int id)
        {
            var dbFirearm = _context.Firearms.SingleOrDefault(x => x.Id == id);
            if (dbFirearm != null)
            {
                _context.Firearms.Remove(dbFirearm);
                _context.SaveChanges();
            }

            return dbFirearm;
        }

        public void Update(Firearm firearm)
        {
            _context.Firearms.Update(firearm);
            _context.SaveChanges();
        }
    }
}
