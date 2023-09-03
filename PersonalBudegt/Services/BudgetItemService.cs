using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PersonalBudget.DataAccess;
using PersonalBudget.Models;
using PersonalBudget.Requests;
using PersonalBudget.Services.Contracts;

namespace PersonalBudget.Services
{
    public class BudgetItemService : IBudgetItemService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private int _userId => int.Parse(_userManager.GetUserId(_httpContextAccessor.HttpContext.User));

        public BudgetItemService(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;

        }
        public async Task<BudgetItem> CreateAsync(CreateBudgetItemRequest budgetItem)
        {
            var isGroupExisted = await _dbContext.BudgetGroups
            .Where(BudgetGroup => BudgetGroup.Plan.UserId == _userId)
            .AnyAsync(BudgetGroup => BudgetGroup.Id == budgetItem.BudgetGroupId);

            if (!isGroupExisted) throw new Exception("Cannot create Budget Item for a non exixted budget group");

            var newBudgetItem = await _dbContext.BudgetItems.AddAsync
                (
                    new BudgetItem
                    {
                        Name = budgetItem.Name,
                        BudgetGroupId = budgetItem.BudgetGroupId,
                        Type = budgetItem.Type,
                        Planned = budgetItem.Planned,
                    }
                );

            await _dbContext.SaveChangesAsync();

            return newBudgetItem.Entity;
        }

        public async Task DeleteAsync(int id)
        {
            var budgetGroup = await _dbContext.BudgetGroups
                .Where(BudgetGroup => BudgetGroup.Plan.UserId == _userId)
                .FirstOrDefaultAsync(BudgetGroup => BudgetGroup.Id == id);

            if (budgetGroup == null) throw new Exception("Cannot Delete this group");

            _dbContext.BudgetGroups.Remove(budgetGroup);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<BudgetItem> UpdateAsync(UpdateBudgetItemRequest request)
        {
            var budgetGroup = await _dbContext.BudgetGroups
                .Where(BudgetGroup => BudgetGroup.Plan.UserId == _userId)
                .AnyAsync(BudgetGroup => BudgetGroup.Id == request.BudgetGroupId);

            if (!budgetGroup) throw new Exception("Cannot Update this group");

            var budgetItem = await _dbContext.BudgetItems
                .FirstOrDefaultAsync(BudgetItem => BudgetItem.Id == request.Id);

            if (budgetItem == null) throw new Exception("Cannot Update this item");

            budgetItem.Name = request.Name;
            budgetItem.Type = request.Type;
            budgetItem.Planned = request.Planned;

            await _dbContext.SaveChangesAsync();

            return budgetItem;
        }
    }
}
