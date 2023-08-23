using Microsoft.AspNetCore.Mvc;
using Moq;
using PersonalBudget.Controllers;
using PersonalBudget.DTO;
using PersonalBudget.Models;
using PersonalBudget.Services.Contracts;

namespace PersonalBudget.Test
{
    public class PlansControllerTest
    {
        [Fact]
        public async Task CreatePlan_PostAction_MustReturnOkObjectOfPlan()
        {
            // Arrange
            var mockPlanService = new Mock<IPlanService>();

            var planDTO = new PlanDTO
            {
                Name = "Test Plan",
                Description = "Test Description",
                TotalPlanned = 1000,
                CreatedAt = DateTime.Now,
            };

            mockPlanService
                .Setup(m => m.CreatePlan(planDTO))
                .ReturnsAsync(new Plan
                {
                    Id = 1,
                    Name = "Test Plan",
                    Description = "Test Description",
                    TotalPlanned = 1000,
                    CreatedAt = DateTime.Now,
                    UserId = 1
                });

            // Act

            var plansController = new PlansController(mockPlanService.Object);

            var result = await plansController.CreatePlan(planDTO);

            // Assert
            var actionResult = Assert.IsType<ActionResult<Plan>>(result);
            Assert.IsType<OkObjectResult>(actionResult.Result);
        }
    }
}