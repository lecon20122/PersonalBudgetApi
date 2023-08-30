using Microsoft.AspNetCore.Mvc;
using Moq;
using PersonalBudget.Controllers;
using PersonalBudget.Models;
using PersonalBudget.Requests;
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

            var planDTO = new CreatePlanRequest
            {
                Name = "Test GetPlanAsync",
                Description = "Test Description",
                TotalPlanned = 1000,
                CreatedAt = DateTime.Now,
            };

            mockPlanService
                .Setup(m => m.CreatePlanAsync(planDTO))
                .ReturnsAsync(new Plan
                {
                    Id = 1,
                    Name = "Test GetPlanAsync",
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

        [Fact]
        public async Task CreatePlan_PostAction_MustReturnBadRequestObjectResult()
        {
            // Arrange
            var mockPlanService = new Mock<IPlanService>();

            var planDTO = new CreatePlanRequest
            {
                Name = "Test GetPlanAsync",
                Description = "Test Description",
                TotalPlanned = 1000,
                CreatedAt = DateTime.Now,
            };

            mockPlanService
                .Setup(m => m.CreatePlanAsync(planDTO))
                .ThrowsAsync(new Exception("Test Exception"));

            // Act

            var plansController = new PlansController(mockPlanService.Object);

            var result = await plansController.CreatePlan(planDTO);

            // Assert
            var actionResult = Assert.IsType<ActionResult<Plan>>(result);
            Assert.IsType<BadRequestObjectResult>(actionResult.Result);
        }

        [Fact]
        public async Task GetPlans_MustReturnEnurableOfPlan()
        {
            // Arrange
            var mockPlanService = new Mock<IPlanService>();

            mockPlanService
                .Setup(m => m.GetPlansAsync())
                .ReturnsAsync(new List<Plan>
                {
                    new Plan
                    {
                        Id = 1,
                        Name = "Test GetPlanAsync",
                        Description = "Test Description",
                        TotalPlanned = 1000,
                        CreatedAt = DateTime.Now,
                        UserId = 1
                    },
                    new Plan
                    {
                        Id = 2,
                        Name = "Test GetPlanAsync 2",
                        Description = "Test Description 2",
                        TotalPlanned = 2000,
                        CreatedAt = DateTime.Now,
                        UserId = 1
                    }
                });

            // Act

            var plansController = new PlansController(mockPlanService.Object);

            var result = await plansController.GetPlans();

            // Assert
            var actionResult = Assert.IsType<ActionResult<IEnumerable<Plan>>>(result);
            Assert.IsType<OkObjectResult>(actionResult.Result);
        }

        [Fact]
        public async Task GetPlan_MustReturnOkObjectOfPlan()
        {
            // Arrange
            var mockPlanService = new Mock<IPlanService>();

            mockPlanService
                .Setup(m => m.GetPlanAsync(1))
                .ReturnsAsync(new Plan
                {
                    Id = 1,
                    Name = "Test GetPlanAsync",
                    Description = "Test Description",
                    TotalPlanned = 1000,
                    CreatedAt = DateTime.Now,
                    UserId = 1
                });

            // Act

            var plansController = new PlansController(mockPlanService.Object);

            var result = await plansController.Plan(1);

            // Assert
            var actionResult = Assert.IsType<ActionResult<Plan>>(result);
            Assert.IsType<OkObjectResult>(actionResult.Result);
        }

        [Fact]
        public async Task GetPlan_MustReturnBadRequestObjectResult()
        {
            // Arrange
            var mockPlanService = new Mock<IPlanService>();

            mockPlanService
                .Setup(m => m.GetPlanAsync(1))
                .ThrowsAsync(new Exception("Test Exception"));

            // Act

            var plansController = new PlansController(mockPlanService.Object);

            var result = await plansController.Plan(1);

            // Assert
            var actionResult = Assert.IsType<ActionResult<Plan>>(result);
            Assert.IsType<BadRequestObjectResult>(actionResult.Result);
        }

        [Fact]
        public async Task UpdatePlan_PutAction_MustReturnOkObjectOfPlan()
        {
            // Arrange
            var mockPlanService = new Mock<IPlanService>();

            var planDTO = new UpdatePlanRequest
            {
                Id = 1,
                Name = "Test GetPlanAsync",
                Description = "Test Description",
                TotalPlanned = 1000,
            };

            mockPlanService
                .Setup(m => m.UpdatePlanAsync(planDTO))
                .ReturnsAsync(new Plan
                {
                    Id = 1,
                    Name = "Test GetPlanAsync",
                    Description = "Test Description",
                    TotalPlanned = 1000,
                    CreatedAt = DateTime.Now,
                    UserId = 1
                });

            // Act

            var plansController = new PlansController(mockPlanService.Object);

            var result = await plansController.UpdatePlan(planDTO);

            // Assert
            var actionResult = Assert.IsType<ActionResult<Plan>>(result);
            Assert.IsType<OkObjectResult>(actionResult.Result);
        }
    }
}