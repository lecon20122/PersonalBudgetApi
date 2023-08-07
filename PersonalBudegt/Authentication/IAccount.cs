using PersonalBudget.Models;

namespace PersonalBudget.Authentication
{
    public interface IAccount
    {
        Task<LoginResult> Login(LoginRequest request);
        Task<ApplicationUser> Register(NewAccountRequest request);
        void Logout();
    }
}
