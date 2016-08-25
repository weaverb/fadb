namespace fadb_api.Models
{
    public class FirearmPart : Entity
    {
        public decimal Cost { get; set; }
        public float Weight { get; set; }
        public string Notes { get; set; }
        public bool IsAvailable { get; set; }
        public int PartTypeId { get; set; }
        public PartType PartType { get; set; }
    }
}
