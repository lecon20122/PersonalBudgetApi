using Microsoft.AspNetCore.Mvc;
using PersonalBudget.Authentication;
using PersonalBudget.General;

namespace PersonalBudget.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccount _account;

        public AccountsController(IAccount account)
        {
            _account = account;
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            try
            {
                var loginResult = await _account.Login(request);
                return Ok(loginResult);
            }
            catch (Exception ex)
            {
                return BadRequest(new Response { IsSuccess = false, Message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Register(NewAccountRequest request)
        {
            try
            {
                var user = await _account.Register(request);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(new Response { IsSuccess = false, Message = ex.Message });
            }
        }
    }
}
