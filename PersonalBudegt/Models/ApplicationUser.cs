using Microsoft.AspNetCore.Identity;

namespace PersonalBudget.Models
{
    public class ApplicationUser : IdentityUser<int>
    {
        public List<Plan> Plans { get; set; }
        public List<Transaction> Transactions { get; set; }
    }
}
