namespace CarSale.Data.Entities
{
    public class EntityBase
    {
        public int Id { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? DeletedDate { get; set; } = DateTime.UtcNow;
        public bool Deleted { get; set; } = false;
    }
}
