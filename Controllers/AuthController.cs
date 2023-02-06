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

        [HttpPost("register")]
        public async Task<ActionResult<ApiResponse<bool>>> Register(RegisterRequest req)
        {
            if (!req.Password.Equals(req.ConfirmPassword))
            {
                return StatusCode(400, new ApiResponse<bool>
                {
                    Result = false,
                    Message = "Password's must be equal"
                });
            }

            var user = await _authService.RegisterAsync(req);
            return StatusCode(200, new ApiResponse<bool>
            {
                Result = true,
                Message = $"Account created for {user.Email}"
            });
        }
    }
}
