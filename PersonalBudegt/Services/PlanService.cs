using Microsoft.AspNetCore.Identity;
using PersonalBudget.DataAccess;
using PersonalBudget.DTO;
using PersonalBudget.Models;
using PersonalBudget.Services.Contracts;

namespace PersonalBudget.Services
{
    public class PlanService : IPlanService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private int _userId => int.Parse(_userManager.GetUserId(_httpContextAccessor.HttpContext.User));

        public PlanService(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<Plan> CreatePlan(PlanDTO plan)
        {

            var plans = _dbContext.Plans.Where(p =>
              (p.CreatedAt >= plan.CreatedAt && p.CreatedAt <= plan.EndedAt) ||
              (p.EndedAt >= plan.CreatedAt && p.EndedAt <= plan.EndedAt) &&
              p.UserId == _userId);




            if (plans.Any())
            {
                throw new Exception("Plan already exists in the same duration");
            }


            var newPlan = new Plan
            {
                Name = plan.Name,
                Description = plan.Description,
                TotalPlanned = plan.TotalPlanned,
                CreatedAt = plan.CreatedAt,
                EndedAt = plan.EndedAt,
                UserId = _userId
            };

            await _dbContext.Plans.AddAsync(newPlan);
            await _dbContext.SaveChangesAsync();

            return newPlan;
        }

        public void DeletePlan(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Plan> GetPlans()
        {
            throw new NotImplementedException();
        }

        public Plan Plan(int id)
        {
            throw new NotImplementedException();
        }

        public Plan UpdatePlan(Plan plan)
        {
            throw new NotImplementedException();
        }
    }
}
