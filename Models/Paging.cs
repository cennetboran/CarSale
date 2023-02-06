namespace CarSale.Models
{
    public class Paging
    {
        public int Page { get; set; } = 1;
        public int Size { get; set; } = 10;
        public string Column { get; set; } = "CreatedDate";
        public bool SortBy { get; set; } = true;
    }
}
