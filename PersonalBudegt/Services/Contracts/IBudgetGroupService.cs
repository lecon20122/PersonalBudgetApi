using PersonalBudget.Models;
using PersonalBudget.Requests;

namespace PersonalBudget.Services.Contracts
{
    public interface IBudgetGroupService
    {
        Task<BudgetGroup> CreateAsync(CreateBudgetGroupRequest budgetGroup);
        Task<BudgetGroup> UpdateGroupNameAsync(UpdateBudgetGroupRequest request);
        Task DeleteAsync(int id);
    }
}
