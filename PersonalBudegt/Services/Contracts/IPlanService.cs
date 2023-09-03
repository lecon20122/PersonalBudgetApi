using PersonalBudget.Models;
using PersonalBudget.Requests;

namespace PersonalBudget.Services.Contracts
{
    public interface IPlanService
    {
        Task<Plan> CreateAsync(CreatePlanRequest plan);
        Task<IEnumerable<Plan>> GetPlansAsync();
        Task<Plan> GetPlanAsync(int id);
        Task<Plan> UpdateAsync(UpdatePlanRequest plan);
        Task DeleteAsync(int id);
    }
}
