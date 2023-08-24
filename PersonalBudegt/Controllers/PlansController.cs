using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalBudget.DTO;
using PersonalBudget.General;
using PersonalBudget.Models;
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
        public string GetPlans()
        {
            try
            {
                return "YES";
            }
            catch (Exception ex)
            {
                return "NO";
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
