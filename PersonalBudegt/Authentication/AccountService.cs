using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using PersonalBudget.DTO;
using PersonalBudget.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PersonalBudget.Authentication
{
    public class AccountService : IAccount
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public AccountService(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }
        public async Task<UserDTO> Login(LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user is null) throw new Exception("Invalid Password or Email");

            var result = await _userManager.CheckPasswordAsync(user, request.Password);

            if (!result) throw new Exception("Invalid Password or Email");

            var claims = new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier , user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var authSignedKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                           issuer: _configuration["JWT:Issuer"],
                           audience: _configuration["JWT:Audience"],
                           claims: claims,
                           expires: DateTime.Now.AddDays(1),
                           signingCredentials: new SigningCredentials(authSignedKey, SecurityAlgorithms.HmacSha256));
            return new UserDTO
            {
                Email = user.Email,
                Name = user.UserName,
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = token.ValidTo
            };
        }

        public void Logout()
        {
            throw new NotImplementedException();
        }

        public async Task<ApplicationUser> Register(NewAccountRequest request)
        {
            var isUserExists = await _userManager.FindByEmailAsync(request.Email);

            if (isUserExists is not null) throw new Exception("User with this Email Address already exists");

            ApplicationUser user = new ApplicationUser
            {
                Email = request.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = request.Name
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded) throw new Exception("Something went wrong");

            return user;
        }
    }
}
