using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonalBudget.General;
using PersonalBudget.Models;
using PersonalBudget.Requests;
using PersonalBudget.Services.Contracts;

namespace PersonalBudget.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BudgetItemsController : ControllerBase
    {
        private readonly IBudgetItemService _budgetItemService;

        public BudgetItemsController(IBudgetItemService budgetItemService)
        {
            _budgetItemService = budgetItemService;
        }

        [HttpPost]
        public async Task<ActionResult<BudgetItem>> CreateAsync([FromForm] CreateBudgetItemRequest request)
        {

            try
            {
                var budgetItem = await _budgetItemService.CreateAsync(request);
                return Ok(budgetItem);

            }
            catch (Exception ex)
            {
                return BadRequest(new Response
                {
                    IsSuccess = false,
                    Message = ex.Message
                });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<BudgetItem>> DeleteAsync(int id)
        {
            try
            {
                await _budgetItemService.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new Response
                {
                    IsSuccess = false,
                    Message = ex.Message
                });
            }
        }

    }
}
