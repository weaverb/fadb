using System;

namespace fadb_api.Models
{
    public class Firearm : Entity
    {
        public string SerialNumber { get; set; }
        public string Caliber { get; set; }
        public decimal Value { get; set; }
        public DateTime AcquiredDate { get; set; }
        public bool IsNfaRegistered { get; set; }
        public int FirearmTypeId { get; set; }
        public FirearmType FirearmType { get; set; }
        public bool IsComplete { get; set; }

    }
}
