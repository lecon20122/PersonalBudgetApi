using PersonalBudget.DTO;
using PersonalBudget.Models;

namespace PersonalBudget.Services.Contracts
{
    public interface IPlanService
    {
        Task<Plan> CreatePlan(PlanDTO plan);
        IEnumerable<Plan> GetPlans();
        Plan Plan(int id);
        Plan UpdatePlan(Plan plan);
        void DeletePlan(int id);
    }
}
