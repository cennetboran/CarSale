using carpass.be.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CarSale.Extensions;
using CarSale.Models;
using CarSale.Data.Entities;
using AutoMapper;

namespace CarSale.Infrastructure
{
    public class AuthService : IAuthService
    {
        protected readonly AppDbContext _context;
        protected readonly AppSettings _appSettings;
        protected readonly IMapper _mapper;

        public AuthService(AppDbContext context, IOptions<AppSettings> appSettings, IMapper mapper)
        {
            _context = context;
            _appSettings = appSettings.Value;
            _mapper = mapper;
        }

        public async Task<User> RegisterAsync(RegisterRequest req)
        {
            var user = _mapper.Map<User>(req);

            user.PasswordHash = req.Password.GetMD5();
            await _context.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<ApiResponse<TokenResult>> LoginAsync(LoginRequest request)
        {
            var response = new ApiResponse<TokenResult>();
            try
            {
                request.Password = request.Password.GetMD5();

                var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == request.Email);

                if (user == null)
                {
                    response.Message = "User not found.";
                    return response;
                }

                if (user.PasswordHash != request.Password)
                {
                    response.Message = "Login credentials incorrect";
                    return response;
                }

                var loginuser = new UserDto
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                };

                string generatedToken = GenerateToken(loginuser);
                loginuser.Token = generatedToken;


                response.Result = new TokenResult
                {
                    Token = generatedToken,
                    Expires = DateTime.UtcNow.AddMinutes(_appSettings.JwtExpire),
                    User = loginuser
                };

                response.Message = "Successfully logged in!";
                return response;
            }
            catch (Exception e)
            {
                response.Message = e.Message;
                response.MessageDetail = e.InnerException != null ? e.InnerException.Message : null;
                return response;
            }


            string GenerateToken(UserDto user)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.JwtKey);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Name, user.Name),
                        new Claim(ClaimTypes.Email, user.Email)
                    }),
                    Expires = DateTime.UtcNow.AddMinutes(_appSettings.JwtExpire),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                            SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
        }
    }
}
