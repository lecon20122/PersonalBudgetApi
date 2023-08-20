using Microsoft.AspNetCore.Identity;

namespace PersonalBudget.Models
{
    public class ApplicationUser : IdentityUser<int>
    {
        public ICollection<Plan> Plans { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
    }
}
