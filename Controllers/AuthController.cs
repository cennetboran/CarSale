using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using CarSale.Data.Entities;
using CarSale.Infrastructure;
using CarSale.Models;

namespace CarSale.Controllers
{
    public class AuthController : BaseController<User>
    {
        protected readonly IAuthService _authService;
        public AuthController(
            IRepository<User> repo,
            IMapper mapper,
            ICurrentUserService currentUserService,
            IAuthService authService) : base(repo, mapper, currentUserService)
        {
            _authService = authService;
        }


        [HttpPost]
        public async Task<ApiResponse<TokenResult>> Login(LoginRequest req)
        {
            return await _authService.LoginAsync(req);
        }
    }
}
