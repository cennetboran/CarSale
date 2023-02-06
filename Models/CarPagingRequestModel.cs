namespace CarSale.Models
{
    public class CarPagingRequestModel : Paging
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public int ModelYear { get; set; }
        public string Color { get; set; }
        public float Price { get; set; }
    }
}
