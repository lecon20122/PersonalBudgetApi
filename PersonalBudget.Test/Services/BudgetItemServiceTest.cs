using Microsoft.EntityFrameworkCore;
using PersonalBudget.DataAccess;
using PersonalBudget.Enums;
using PersonalBudget.Models;
using PersonalBudget.Requests;
using PersonalBudget.Services;
using PersonalBudget.Services.Contracts;
using PersonalBudget.Test.Fixtures;

namespace PersonalBudget.Test.Services
{
    public class BudgetItemServiceTest : IClassFixture<TestDatabaseFixture>, IClassFixture<AuthenticatedUserFixture>
    {
        private readonly TestDatabaseFixture _testDatabase;
        private readonly AuthenticatedUserFixture _authenticatedUserFixture;
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IBudgetGroupService _BudgetGroupService;
        private readonly IBudgetItemService _BudgetItemService;
        private readonly IPlanService _planService;

        public BudgetItemServiceTest(TestDatabaseFixture testDatabase, AuthenticatedUserFixture authenticatedUserFixture)
        {
            _testDatabase = testDatabase;
            _authenticatedUserFixture = authenticatedUserFixture;
            _applicationDbContext = _testDatabase.CreateContext();
            _BudgetGroupService = new BudgetGroupService(_applicationDbContext,
                _authenticatedUserFixture.MockUserManager.Object,
                _authenticatedUserFixture.MockHttpContextAccessor.Object);
            _BudgetItemService = new BudgetItemService(_applicationDbContext,
                               _authenticatedUserFixture.MockUserManager.Object,
                                              _authenticatedUserFixture.MockHttpContextAccessor.Object);

            _planService = new PlanService(_applicationDbContext,
                               _authenticatedUserFixture.MockUserManager.Object,
                                              _authenticatedUserFixture.MockHttpContextAccessor.Object);

        }


        public async Task<BudgetGroup> CreateBudgetGroupHelper()
        {
            var plan = await _planService.CreateAsync(new CreatePlanRequest
            {
                Name = "Test GetPlanAsync",
                Description = "Test Description",
                TotalPlanned = 1000,
                CreatedAt = DateTime.Now,
            });

            var BudgetGroupDTO = new CreateBudgetGroupRequest
            {
                Name = "Test GetBudgetGroupAsync",
                PlanId = plan.Id,
            };
            return await _BudgetGroupService.CreateAsync(BudgetGroupDTO);
        }

        [Fact]
        public async Task Authenticated_User_Can_Create_BudgetItem()
        {
            // Arrange
            BudgetGroup budgetGroup = await CreateBudgetGroupHelper();

            // Act
            var BudgetItemRequest = new CreateBudgetItemRequest
            {
                Name = "Test GetBudgetItemAsync",
                BudgetGroupId = budgetGroup.Id,
                Planned = 1000,
                Type = BudgetType.Expense,
            };

            // Act
            var newBudgetItem = await _BudgetItemService.CreateAsync(BudgetItemRequest);

            // Assert

            //Assert the budget item is created and in the database
            var isBudgetItemExisted = await _applicationDbContext.BudgetItems.AnyAsync(b => b.Id == newBudgetItem.Id);

            Assert.True(isBudgetItemExisted);

            Assert.Equal(BudgetItemRequest.Name, newBudgetItem.Name);
            Assert.Equal(BudgetItemRequest.BudgetGroupId, newBudgetItem.BudgetGroupId);
        }

        [Fact]
        public async Task Cannot_Create_BudgetItem_For_None_Existed_BudgetGroup()
        {
            // Arrange
            var BudgetItemRequest = new CreateBudgetItemRequest
            {
                Name = "Test GetBudgetItemAsync",
                BudgetGroupId = 999,
                Planned = 1000,
                Type = BudgetType.Expense,
            };

            // Act
            // Assert
            // Assert Exception
            Assert.ThrowsAsync<Exception>(async () => await _BudgetItemService.CreateAsync(BudgetItemRequest));
        }

        [Fact]
        public async Task Authenticated_User_Can_Delete_BudgetItem()
        {
            // Arrange
            BudgetGroup budgetGroup = await CreateBudgetGroupHelper();

            var BudgetItemRequest = new CreateBudgetItemRequest
            {
                Name = "Test GetBudgetItemAsync",
                BudgetGroupId = budgetGroup.Id,
                Planned = 1000,
                Type = BudgetType.Expense,
            };

            // Act
            var newBudgetItem = await _BudgetItemService.CreateAsync(BudgetItemRequest);

            // Act
            await _BudgetItemService.DeleteAsync(newBudgetItem.Id);

            // Assert
            //Assert the budget item is deleted and not in the database
            bool isBudgetItemExisted = await _applicationDbContext.BudgetItems.AnyAsync(b => b.Id == newBudgetItem.Id);

            Assert.False(isBudgetItemExisted);
        }

        [Fact]
        public async Task Cannot_Delete_BudgetItem_For_None_Existed_BudgetItem()
        {
            // Arrange
            // Act
            // Assert
            // Assert Exception
            Assert.ThrowsAsync<Exception>(async () => await _BudgetItemService.DeleteAsync(999));
        }

        //update

    }
}
