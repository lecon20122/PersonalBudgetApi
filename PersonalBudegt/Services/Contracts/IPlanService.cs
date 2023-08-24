using PersonalBudget.DTO;
using PersonalBudget.Models;

namespace PersonalBudget.Services.Contracts
{
    public interface IPlanService
    {
        Task<Plan> CreatePlan(CreatePlanRequest plan);
        IEnumerable<Plan> GetPlans();
        Plan Plan(int id);
        Plan UpdatePlan(Plan plan);
        Task DeletePlan(int id);
    }
}
