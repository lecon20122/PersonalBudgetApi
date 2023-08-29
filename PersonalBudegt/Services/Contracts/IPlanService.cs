using PersonalBudget.Models;
using PersonalBudget.Requests;

namespace PersonalBudget.Services.Contracts
{
    public interface IPlanService
    {
        Task<Plan> CreatePlan(CreatePlanRequest plan);
        Task<IEnumerable<Plan>> GetPlans();
        Plan Plan(int id);
        Plan UpdatePlan(Plan plan);
        Task DeletePlan(int id);
    }
}
