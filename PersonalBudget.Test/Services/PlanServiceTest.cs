using PersonalBudget.DataAccess;
using PersonalBudget.DTO;
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
                Name = "Test Plan",
                Description = "Test Description",
                TotalPlanned = 1000,
                CreatedAt = DateTime.Now,
            };

            // Act
            var newPaln = await _planService.CreatePlan(planDTO);

            // Assert
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
                Name = "Test Plan",
                Description = "Test Description",
                TotalPlanned = 1000,
                CreatedAt = DateTime.Now.AddMonths(1),
            };

            // Act

            var resultFirstTime = await _planService.CreatePlan(planDTO);

            // Assert
            //Expects throw new Exception("Plan already exists in the same duration");
            await Assert.ThrowsAsync<Exception>(async () => await _planService.CreatePlan(planDTO));
        }

        [Fact]
        public async Task Authenticated_User_Can_Delete_Plan()
        {
            // Arrange
            var planDTO = new CreatePlanRequest
            {
                Name = "Test Plan",
                Description = "Test Description",
                TotalPlanned = 1000,
                CreatedAt = DateTime.Now.AddMonths(2),
            };

            var newPaln = await _planService.CreatePlan(planDTO);

            // Act
            await _planService.DeletePlan(newPaln.Id);

            // Assert
            Assert.Null(_applicationDbContext.Plans.FirstOrDefault(p => p.Id == newPaln.Id));
        }

        [Fact]
        public async Task Cannot_Delete_Plan_That_Does_Not_Exist()
        {
            // Arrange
            // Act
            // Assert
            //Expects throw new Exception("Plan not found");
            await Assert.ThrowsAsync<Exception>(async () => await _planService.DeletePlan(16));
        }
    }
}
