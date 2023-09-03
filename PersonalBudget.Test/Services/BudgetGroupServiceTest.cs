using Microsoft.EntityFrameworkCore;
using PersonalBudget.DataAccess;
using PersonalBudget.Requests;
using PersonalBudget.Services;
using PersonalBudget.Services.Contracts;
using PersonalBudget.Test.Fixtures;

namespace PersonalBudget.Test.Services
{
    public class BudgetGroupServiceTest : IClassFixture<TestDatabaseFixture>, IClassFixture<AuthenticatedUserFixture>
    {
        private readonly TestDatabaseFixture _testDatabase;
        private readonly AuthenticatedUserFixture _authenticatedUserFixture;
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IBudgetGroupService _BudgetGroupService;
        private readonly IPlanService _planService;

        public BudgetGroupServiceTest(TestDatabaseFixture testDatabase, AuthenticatedUserFixture authenticatedUserFixture)
        {
            _testDatabase = testDatabase;
            _authenticatedUserFixture = authenticatedUserFixture;
            _applicationDbContext = _testDatabase.CreateContext();
            _BudgetGroupService = new BudgetGroupService(_applicationDbContext,
                _authenticatedUserFixture.MockUserManager.Object,
                _authenticatedUserFixture.MockHttpContextAccessor.Object);

            _planService = new PlanService(_applicationDbContext,
                   _authenticatedUserFixture.MockUserManager.Object,
                                  _authenticatedUserFixture.MockHttpContextAccessor.Object);
        }


        [Fact]
        public async Task Authenticated_User_Can_Create_BudgetGroup()
        {
            // Arrange
            var planRequest = new CreatePlanRequest
            {
                Name = "Test GetPlanAsync",
                Description = "Test Description",
                TotalPlanned = 1000,
                CreatedAt = DateTime.Now,
            };

            // Act
            var newPaln = await _planService.CreateAsync(planRequest);


            var BudgetGroupDTO = new CreateBudgetGroupRequest
            {
                Name = "Test GetBudgetGroupAsync",
                PlanId = newPaln.Id,
            };

            // Act
            var newBudgetGroup = await _BudgetGroupService.CreateAsync(BudgetGroupDTO);

            // Assert

            //Assert the budget group is created and in the database
            var BudgetGroup = await _applicationDbContext.BudgetGroups.AnyAsync(x => x.Id == newBudgetGroup.Id);
            Assert.True(BudgetGroup);

            Assert.Equal(BudgetGroupDTO.Name, newBudgetGroup.Name);
            Assert.Equal(BudgetGroupDTO.PlanId, newBudgetGroup.PlanId);

        }

        [Fact]
        public async Task Cannot_Create_BudgetGroup_For_None_Existed_Plan()
        {
            // Arrange

            var BudgetGroupDTO = new CreateBudgetGroupRequest
            {
                Name = "Test GetBudgetGroupAsync",
                PlanId = 999,
            };

            // Act
            // Assert Exception
            Assert.ThrowsAsync<Exception>(async () => await _BudgetGroupService.CreateAsync(BudgetGroupDTO));
        }


        [Fact]
        public async Task Authenticated_User_Can_Delete_BudgetGroup()
        {
            // Arrange
            var planRequest = new CreatePlanRequest
            {
                Name = "Test GetPlanAsync",
                Description = "Test Description",
                TotalPlanned = 1000,
                CreatedAt = DateTime.Now,
            };

            var newPaln = await _planService.CreateAsync(planRequest);

            var BudgetGroupRequest = new CreateBudgetGroupRequest
            {
                Name = "Test GetBudgetGroupAsync",
                PlanId = newPaln.Id,
            };

            var newBudgetGroup = await _BudgetGroupService.CreateAsync(BudgetGroupRequest);

            // Act

            await _BudgetGroupService.DeleteAsync(newBudgetGroup.Id);

            // Assert the budget group is deleted

            var BudgetGroup = await _applicationDbContext.BudgetGroups.AnyAsync(x => x.Id == newBudgetGroup.Id);

            Assert.False(BudgetGroup);
        }

    }
}
