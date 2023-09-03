using Microsoft.AspNetCore.Mvc;
using PersonalBudget.General;
using PersonalBudget.Models;
using PersonalBudget.Requests;
using PersonalBudget.Services.Contracts;

namespace PersonalBudget.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BudgetGroupsController : ControllerBase
    {
        private readonly IBudgetGroupService _budgetGroupService;

        public BudgetGroupsController(IBudgetGroupService budgetGroupService)
        {
            _budgetGroupService = budgetGroupService;
        }

        [HttpPost]
        public async Task<ActionResult<BudgetGroup>> CreateAsync([FromBody] CreateBudgetGroupRequest budgetGroup)
        {
            try
            {
                var newBudgetGroup = await _budgetGroupService.CreateAsync(budgetGroup);
                return Ok(newBudgetGroup);
            }
            catch (Exception ex)
            {
                return BadRequest(new Response { IsSuccess = false, Message = ex.Message });
            }
        }

        [HttpPut]
        public async Task<ActionResult<BudgetGroup>> UpdateGroupNameAsync([FromBody] UpdateBudgetGroupRequest request)
        {
            try
            {
                var updatedGroup = await _budgetGroupService.UpdateGroupNameAsync(request);
                return Ok(updatedGroup);
            }
            catch (Exception ex)
            {
                return BadRequest(new Response { IsSuccess = false, Message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            try
            {
                await _budgetGroupService.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new Response { IsSuccess = false, Message = ex.Message });
            }
        }
    }
}
