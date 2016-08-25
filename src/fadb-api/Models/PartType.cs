namespace fadb_api.Models
{
    public class PartType : Entity
    {
        public int FirearmTypeId { get; set; }
        public FirearmType FirearmType { get; set; }
    }
}
