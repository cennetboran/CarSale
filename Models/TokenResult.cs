namespace CarSale.Models
{
    public class TokenResult
    {
        public string Token { get; set; }
        public DateTime Expires { get; set; }
        public UserDto User { get; set; }
    }
}
