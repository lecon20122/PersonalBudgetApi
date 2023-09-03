using PersonalBudget.Models;
using PersonalBudget.Requests;

namespace PersonalBudget.Services.Contracts
{
    public interface IBudgetItemService
    {
        Task<BudgetItem> CreateAsync(CreateBudgetItemRequest budgetItem);
        Task DeleteAsync(int id);
        Task<BudgetItem> UpdateAsync(UpdateBudgetItemRequest request);
    }
}
