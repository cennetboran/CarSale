using CarSale.Models;

namespace CarSale.Infrastructure
{
    public interface IAuthService
    {
        Task<ApiResponse<TokenResult>> LoginAsync(LoginRequest request);
    }
}
