namespace CarSale.Data.Entities
{
    public class Car : EntityBase
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public int ModelYear { get; set; }
        public string Color { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public CarFeature CarFeature { get; set; }
        public int CarFeatureId { get; set; }

        public User User { get; set; }
        public int UserId { get; set; }
    }
}
