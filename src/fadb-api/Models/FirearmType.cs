using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fadb_api.Models
{
    public class FirearmType : Entity
    {
        public string Notes { get; set; }
         public IEnumerable<PartType> RequiredParts { get; set; }

    }
}
