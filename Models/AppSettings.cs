namespace CarSale.Models
{
    public class AppSettings
    {
        public string SqlConnection { get; set; }
        public string JwtKey { get; set; }
        public int JwtExpire { get; set; }
    }
}
