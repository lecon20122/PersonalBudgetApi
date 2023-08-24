using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
        public async Task<Plan> CreatePlan(CreatePlanRequest plan)
        {
            var plans = _dbContext.Plans
                .Where(p => p.UserId == _userId)
                .Where(p => p.CreatedAt.Month == plan.CreatedAt.Month);

            if (plans.Any())
            {
                throw new Exception("You already have a plan for this month");
            }

            var newPlan = new Plan
            {
                Name = plan.Name,
                Description = plan.Description,
                TotalPlanned = plan.TotalPlanned,
                CreatedAt = plan.CreatedAt,
                UserId = _userId
            };

            await _dbContext.Plans.AddAsync(newPlan);
            await _dbContext.SaveChangesAsync();

            return newPlan;
        }

        public async Task DeletePlan(int id)
        {
            var plan = await _dbContext.Plans
                .Where(p => p.UserId == _userId)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (plan == null)
            {
                throw new Exception("Plan not found");
            }

            _dbContext.Plans.Remove(plan);
            await _dbContext.SaveChangesAsync();
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
