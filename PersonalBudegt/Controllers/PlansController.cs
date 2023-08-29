using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalBudget.General;
using PersonalBudget.Models;
using PersonalBudget.Requests;
using PersonalBudget.Services.Contracts;

namespace PersonalBudget.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class PlansController : ControllerBase
    {
        private readonly IPlanService _plans;

        public PlansController(IPlanService plans)
        {
            _plans = plans;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Plan>>> GetPlans()
        {
            try
            {
                var plans = await _plans.GetPlans();
                return Ok(plans);
            }
            catch (Exception ex)
            {
                return BadRequest(new Response { IsSuccess = false, Message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<Plan>> CreatePlan([FromBody] CreatePlanRequest plan)
        {
            try
            {
                var newPlan = await _plans.CreatePlan(plan);
                return Ok(newPlan);
            }
            catch (Exception ex)
            {
                return BadRequest(new Response { IsSuccess = false, Message = ex.Message });
            }
        }
    }
}
