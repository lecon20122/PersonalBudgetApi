using Microsoft.EntityFrameworkCore;
using PersonalBudget.DataAccess;
using PersonalBudget.Requests;
using PersonalBudget.Services;
using PersonalBudget.Services.Contracts;
using PersonalBudget.Test.Fixtures;

namespace PersonalBudget.Test.Services
{
    public class PlanServiceTest : IClassFixture<TestDatabaseFixture>, IClassFixture<AuthenticatedUserFixture>
    {
        private readonly TestDatabaseFixture _testDatabase;
        private readonly AuthenticatedUserFixture _authenticatedUserFixture;
        private readonly IPlanService _planService;
        private readonly ApplicationDbContext _applicationDbContext;
        public PlanServiceTest(TestDatabaseFixture testDatabase, AuthenticatedUserFixture authenticatedUserFixture)
        {
            _testDatabase = testDatabase;
            _authenticatedUserFixture = authenticatedUserFixture;
            _applicationDbContext = _testDatabase.CreateContext();
            _planService = new PlanService(_applicationDbContext,
                               _authenticatedUserFixture.MockUserManager.Object,
                                              _authenticatedUserFixture.MockHttpContextAccessor.Object);
        }


        [Fact]
        public async Task Authenticated_User_Can_Create_Plan()
        {
            // Arrange

            var planDTO = new CreatePlanRequest
            {
                Name = "Test GetPlanAsync",
                Description = "Test Description",
                TotalPlanned = 1000,
                CreatedAt = DateTime.Now,
            };

            // Act
            var newPaln = await _planService.CreateAsync(planDTO);

            // Assert
            // Assert the plan is created and in the database

            var plan = await _applicationDbContext.Plans.AnyAsync(p => p.Id == newPaln.Id);

            Assert.True(plan);

            Assert.Equal(planDTO.Name, newPaln.Name);
            Assert.Equal(planDTO.TotalPlanned, newPaln.TotalPlanned);
            Assert.Equal(planDTO.CreatedAt, newPaln.CreatedAt);

        }


        [Fact]
        public async Task Cannot_Create_Plan_Twice_For_The_Same_Month()
        {
            // Arrange

            var planDTO = new CreatePlanRequest
            {
                Name = "Test GetPlanAsync",
                Description = "Test Description",
                TotalPlanned = 1000,
                CreatedAt = DateTime.Now.AddMonths(1),
            };

            // Act

            var resultFirstTime = await _planService.CreateAsync(planDTO);

            // Assert
            //Expects throw new Exception("GetPlanAsync already exists in the same duration");
            await Assert.ThrowsAsync<Exception>(async () => await _planService.CreateAsync(planDTO));
        }

        [Fact]
        public async Task Authenticated_User_Can_Delete_Plan()
        {
            // Arrange
            var planDTO = new CreatePlanRequest
            {
                Name = "Test GetPlanAsync",
                Description = "Test Description",
                TotalPlanned = 1000,
                CreatedAt = DateTime.Now.AddMonths(2),
            };

            var newPaln = await _planService.CreateAsync(planDTO);

            // Act
            await _planService.DeleteAsync(newPaln.Id);

            // Assert
            Assert.Null(_applicationDbContext.Plans.FirstOrDefault(p => p.Id == newPaln.Id));
        }

        [Fact]
        public async Task Cannot_Delete_Plan_That_Does_Not_Exist()
        {
            // Arrange
            // Act
            // Assert
            //Expects throw new Exception("GetPlanAsync not found");
            await Assert.ThrowsAsync<Exception>(async () => await _planService.DeleteAsync(16));
        }

        [Fact]
        public async Task Authenticated_User_Can_Update_Plan()
        {
            // Arrange
            var planRequest = new CreatePlanRequest
            {
                Name = "Test GetPlanAsync",
                Description = "Test Description",
                TotalPlanned = 1000,
                CreatedAt = DateTime.Now.AddMonths(3),
            };

            var newPaln = await _planService.CreateAsync(planRequest);

            var updatePlanRequest = new UpdatePlanRequest
            {
                Id = newPaln.Id,
                Name = "Test GetPlanAsync Updated",
                Description = "Test Description Updated",
                TotalPlanned = 2000,
            };

            // Act
            var updatedPlan = await _planService.UpdateAsync(updatePlanRequest);

            // Assert
            Assert.Equal(updatePlanRequest.Name, updatedPlan.Name);
            Assert.Equal(updatePlanRequest.TotalPlanned, updatedPlan.TotalPlanned);
            Assert.Equal(updatePlanRequest.Description, updatedPlan.Description);
        }

        [Fact]
        public async Task Cannot_Update_Plan_That_Does_Not_Exist()
        {
            // Arrange
            var updatePlanRequest = new UpdatePlanRequest
            {
                Id = 16,
                Name = "Test GetPlanAsync Updated",
                Description = "Test Description Updated",
                TotalPlanned = 2000,
            };

            // Act
            // Assert
            //Expects throw new Exception("GetPlanAsync not found");
            await Assert.ThrowsAsync<Exception>(async () => await _planService.UpdateAsync(updatePlanRequest));
        }

        [Fact]
        public async Task GetPlan_Authenticated_User_Can_Get_His_Plan_By_ID()
        {
            // Arrange
            var planRequest = new CreatePlanRequest
            {
                Name = "Test GetPlanAsync",
                Description = "Test Description",
                TotalPlanned = 1000,
                CreatedAt = DateTime.Now.AddMonths(3),
            };

            var newPaln = await _planService.CreateAsync(planRequest);

            // Act
            var plan = await _planService.GetPlanAsync(newPaln.Id);

            // Assert

            Assert.Equal(planRequest.Name, plan.Name);
            Assert.Equal(planRequest.TotalPlanned, plan.TotalPlanned);
            Assert.Equal(planRequest.Description, plan.Description);
        }

        [Fact]
        public async Task Cannot_Get_Plan_That_Does_Not_Exist()
        {
            // Arrange
            // Act
            // Assert
            //Expects throw new Exception("GetPlanAsync not found");
            await Assert.ThrowsAsync<Exception>(async () => await _planService.GetPlanAsync(16));
        }

        [Fact]
        public async Task GetPlans_Authenticated_User_Can_Get_His_Plans()
        {
            // Arrange
            var planRequest = new CreatePlanRequest
            {
                Name = "Test GetPlanAsync",
                Description = "Test Description",
                TotalPlanned = 1000,
                CreatedAt = DateTime.Now.AddMonths(4),
            };

            var planRequest2 = new CreatePlanRequest
            {
                Name = "Test GetPlanAsync",
                Description = "Test Description",
                TotalPlanned = 1000,
                CreatedAt = DateTime.Now.AddMonths(3),
            };

            var newPaln = await _planService.CreateAsync(planRequest);
            var newPaln2 = await _planService.CreateAsync(planRequest2);

            // Act
            var plans = await _planService.GetPlansAsync();

            // Assert
            Assert.True(plans.Count() == 2);
        }
    }
}
