using Microsoft.AspNetCore.Mvc;
using BankingSystem;

namespace BankingSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BankingController : ControllerBase
    {
        private readonly IBankingService _bankingService;

        public BankingController(IBankingService bankingService)
        {
            _bankingService = bankingService;
        }

        [HttpPost("users")]
        public async Task<ActionResult<User>> CreateUser([FromBody] string name)
        {
            try
            {
                var user = await _bankingService.CreateUserAsync(name);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("users/{userId}")]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            try
            {
                await _bankingService.DeleteUserAsync(userId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("users/{userId}/accounts")]
        public async Task<ActionResult<Account>> CreateAccount(string userId)
        {
            try
            {
                var account = await _bankingService.CreateAccountAsync(userId);
                return Ok(account);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("users/{userId}/accounts/{accountId}")]
        public async Task<IActionResult> DeleteAccount(string userId, string accountId)
        {
            try
            {
                await _bankingService.DeleteAccountAsync(userId, accountId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("users/{userId}/accounts/{accountId}/deposit")]
        public async Task<IActionResult> Deposit(string userId, string accountId, [FromBody] decimal amount)
        {
            try
            {
                await _bankingService.DepositAsync(userId, accountId, amount);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("users/{userId}/accounts/{accountId}/withdraw")]
        public async Task<IActionResult> Withdraw(string userId, string accountId, [FromBody] decimal amount)
        {
            try
            {
                await _bankingService.WithdrawAsync(userId, accountId, amount);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
