using PersonalBudget.DTO;
using PersonalBudget.Models;

namespace PersonalBudget.Authentication
{
    public interface IAccount
    {
        Task<UserDTO> Login(LoginRequest request);
        Task<ApplicationUser> Register(NewAccountRequest request);
        void Logout();
    }
}
