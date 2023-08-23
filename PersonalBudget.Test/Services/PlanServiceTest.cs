using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Moq;
using PersonalBudget.DTO;
using PersonalBudget.Models;
using PersonalBudget.Services;
using PersonalBudget.Services.Contracts;
using PersonalBudget.Test.Fixtures;
using System.Security.Claims;

namespace PersonalBudget.Test.Services
{
    public class PlanServiceTest : IClassFixture<TestDatabaseFixture>
    {
        private readonly TestDatabaseFixture _testDatabase;

        public PlanServiceTest(TestDatabaseFixture testDatabase)
        {
            _testDatabase = testDatabase;
        }

        [Fact]
        public async Task Cannot_Create_Plan_Twice_For_The_Same_Month()
        {
            // Arrange

            var store = new Mock<IUserStore<ApplicationUser>>();

            var mockUserManager = new Mock<UserManager<ApplicationUser>>(store.Object, null, null, null, null, null, null, null, null);

            mockUserManager.Setup
                (m => m.GetUserId(It.IsAny<ClaimsPrincipal>()))
                    .Returns("1");

            var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();

            mockHttpContextAccessor.Setup
                (m => m.HttpContext.User)
                    .Returns(new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, "1"),
                        new Claim(ClaimTypes.Name, "Mustafa"),
                    }, "mock")));

            var dbContext = _testDatabase.CreateContext();

            var planDTO = new PlanDTO
            {
                Name = "Test Plan",
                Description = "Test Description",
                TotalPlanned = 1000,
                CreatedAt = DateTime.Now,
            };

            IPlanService planService = new PlanService(dbContext, mockUserManager.Object, mockHttpContextAccessor.Object);

            // Act

            var resultFirstTime = await planService.CreatePlan(planDTO);

            // Assert
            //Expects throw new Exception("Plan already exists in the same duration");
            await Assert.ThrowsAsync<Exception>(async () => await planService.CreatePlan(planDTO));
        }
    }
}
