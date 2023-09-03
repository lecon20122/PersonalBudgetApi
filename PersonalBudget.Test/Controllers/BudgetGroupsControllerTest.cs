using Microsoft.AspNetCore.Mvc;
using Moq;
using PersonalBudget.Controllers;
using PersonalBudget.Models;
using PersonalBudget.Requests;
using PersonalBudget.Services.Contracts;

namespace PersonalBudget.Test.Controllers
{
    public class BudgetGroupsControllerTest
    {
        [Fact]
        public async Task CreateAsync_PostAction_MustReturnOkObjectOfBudgetGroup()
        {
            // Arrange
            var mockBudgetGroupService = new Mock<IBudgetGroupService>();

            var budgetGroupRequest = new CreateBudgetGroupRequest
            {
                Name = "Test GetBudgetGroupAsync",
                PlanId = 1,
            };

            mockBudgetGroupService
                .Setup(m => m.CreateAsync(budgetGroupRequest))
                .ReturnsAsync(new BudgetGroup
                {
                    Id = 1,
                    Name = "Test GetBudgetGroupAsync",
                    CreatedAt = DateTime.Now,
                    PlanId = 1,
                    UpdatedAt = DateTime.Now,
                });

            // Act

            var budgetGroupsController = new BudgetGroupsController(mockBudgetGroupService.Object);

            var result = await budgetGroupsController.CreateAsync(budgetGroupRequest);

            // Assert
            var actionResult = Assert.IsType<ActionResult<BudgetGroup>>(result);
            Assert.IsType<OkObjectResult>(actionResult.Result);
        }

        [Fact]
        public async Task CreateAsync_PostAction_MustReturnBadRequestObjectResult()
        {
            // Arrange
            var mockBudgetGroupService = new Mock<IBudgetGroupService>();

            var budgetGroupRequest = new CreateBudgetGroupRequest
            {
                Name = "Test GetBudgetGroupAsync",
                PlanId = 1,
            };

            mockBudgetGroupService
                .Setup(m => m.CreateAsync(budgetGroupRequest))
                .ThrowsAsync(new Exception("Test Exception"));

            // Act

            var budgetGroupsController = new BudgetGroupsController(mockBudgetGroupService.Object);

            var result = await budgetGroupsController.CreateAsync(budgetGroupRequest);

            // Assert
            var actionResult = Assert.IsType<ActionResult<BudgetGroup>>(result);
            Assert.IsType<BadRequestObjectResult>(actionResult.Result);
        }

        [Fact]
        public async Task UpdateGroupNameAsync_PutAction_MustReturnOkObjectOfBudgetGroup()
        {
            // Arrange
            var mockBudgetGroupService = new Mock<IBudgetGroupService>();

            var budgetGroupRequest = new UpdateBudgetGroupRequest
            {
                Id = 1,
                Name = "Test GetBudgetGroupAsync",
            };


            mockBudgetGroupService
                .Setup(m => m.UpdateGroupNameAsync(budgetGroupRequest))
                .ReturnsAsync(new BudgetGroup
                {
                    Id = 1,
                    Name = "Test GetBudgetGroupAsync",
                    CreatedAt = DateTime.Now,
                    PlanId = 1,
                    UpdatedAt = DateTime.Now,
                });

            // Act

            var budgetGroupsController = new BudgetGroupsController(mockBudgetGroupService.Object);

            var result = await budgetGroupsController.UpdateGroupNameAsync(budgetGroupRequest);

            // Assert
            var actionResult = Assert.IsType<ActionResult<BudgetGroup>>(result);
            Assert.IsType<OkObjectResult>(actionResult.Result);
        }

        [Fact]
        public async Task UpdateGroupNameAsync_PutAction_MustReturnBadRequestObjectResult()
        {
            // Arrange
            var mockBudgetGroupService = new Mock<IBudgetGroupService>();

            var budgetGroupRequest = new UpdateBudgetGroupRequest
            {
                Id = 1,
                Name = "Test GetBudgetGroupAsync",
            };

            mockBudgetGroupService
                .Setup(m => m.UpdateGroupNameAsync(budgetGroupRequest))
                .ThrowsAsync(new Exception("Test Exception"));

            // Act

            var budgetGroupsController = new BudgetGroupsController(mockBudgetGroupService.Object);

            var result = await budgetGroupsController.UpdateGroupNameAsync(budgetGroupRequest);

            // Assert
            var actionResult = Assert.IsType<ActionResult<BudgetGroup>>(result);
            Assert.IsType<BadRequestObjectResult>(actionResult.Result);
        }

        [Fact]
        public async Task DeleteAsync_DeleteAction_MustReturnOkObject()
        {
            // Arrange
            var mockBudgetGroupService = new Mock<IBudgetGroupService>();

            mockBudgetGroupService
                .Setup(m => m.DeleteAsync(1))
                .Verifiable();

            // Act

            var budgetGroupsController = new BudgetGroupsController(mockBudgetGroupService.Object);

            var result = await budgetGroupsController.DeleteAsync(1);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task DeleteAsync_DeleteAction_MustReturnBadRequestObjectResult()
        {
            // Arrange
            var mockBudgetGroupService = new Mock<IBudgetGroupService>();

            mockBudgetGroupService
                .Setup(m => m.DeleteAsync(1))
                .ThrowsAsync(new Exception("Test Exception"));

            // Act

            var budgetGroupsController = new BudgetGroupsController(mockBudgetGroupService.Object);

            var result = await budgetGroupsController.DeleteAsync(1);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
