using fadb_api.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fadb_api.ef
{
    public class FirearmDbContext : DbContext
    {
        public FirearmDbContext(DbContextOptions<FirearmDbContext> options) : base(options)
        {

        }

        public DbSet<Firearm> Firearms { get; set; }
        public DbSet<FirearmType> FirearmTypes { get; set; }
        public DbSet<FirearmPart> FirearmParts { get; set; }
        public DbSet<PartType> PartTypes { get; set; }

    }
}
