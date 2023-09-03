
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PersonalBudget.DataAccess;
using PersonalBudget.Models;
using PersonalBudget.Requests;
using PersonalBudget.Services.Contracts;

namespace PersonalBudget.Services
{
    public class BudgetGroupService : IBudgetGroupService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private int _userId => int.Parse(_userManager.GetUserId(_httpContextAccessor.HttpContext.User));

        public BudgetGroupService(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<BudgetGroup> CreateAsync(CreateBudgetGroupRequest budgetGroup)
        {
            Plan plan = await _dbContext.Plans
                .Where(p => p.UserId == _userId)
                .FirstOrDefaultAsync(p => p.Id == budgetGroup.PlanId);

            if (plan == null) throw new Exception("Cannot create Budget Group for a non exixted plan");

            var newBudgetGroup = await _dbContext.BudgetGroups.AddAsync
                (
                    new BudgetGroup
                    {
                        Name = budgetGroup.Name,
                        PlanId = budgetGroup.PlanId,
                    }
                );

            await _dbContext.SaveChangesAsync();

            return newBudgetGroup.Entity;
        }

        public async Task DeleteAsync(int id)
        {
            var group = await _dbContext.BudgetGroups
                         .Select(BudgetGroup => new
                         {
                             BudgetGroup = BudgetGroup,
                             UserId = BudgetGroup.Plan.UserId
                         })
                        .FirstOrDefaultAsync(g => g.BudgetGroup.Id == id);

            if (group == null || group.UserId != _userId) throw new Exception("Cannot Delete this group");

            _dbContext.Remove(group.BudgetGroup);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<BudgetGroup> UpdateGroupNameAsync(UpdateBudgetGroupRequest request)
        {
            var group = await _dbContext.BudgetGroups
            .Include(b => b.Plan)
            .FirstOrDefaultAsync(g => g.Id == request.Id);

            if (group == null || group.Plan.UserId != _userId) throw new Exception("Cannot Update this group");

            group.Name = request.Name;

            await _dbContext.SaveChangesAsync();

            return group;
        }
    }
}
