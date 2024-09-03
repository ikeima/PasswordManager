using Microsoft.AspNetCore.Mvc;
using PasswordManager.Models;
using PasswordManager.Services;

namespace PasswordManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PasswordFeedbackController : ControllerBase
    {
        private readonly IPasswordFeedbackService _passwordFeedbackService;

        public PasswordFeedbackController(IPasswordFeedbackService passwordFeedbackService)
        {
            _passwordFeedbackService = passwordFeedbackService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddNewCombination([FromBody] PasswordFeedback combination)
        {
            var result = await _passwordFeedbackService.AddNewCombinationAsync(combination);
            if (result)
                return Ok("Успешно добавлено");
            return BadRequest("Комбинация с таким паролем уже существует");
        }

        [HttpGet("count")]
        public async Task<IActionResult> GetCount()
        {
            var count = await _passwordFeedbackService.GetCountAsync();
            return Ok(count);
        }

        [HttpGet("hash")]
        public async Task<IActionResult> GetHashByFeedback([FromQuery] string feedback)
        {
            var hash = await _passwordFeedbackService.GetHashByFeedbackAsync(feedback);
            if (hash != null)
                return Ok(hash);
            return NotFound("Пароль с таким отзывом не найден");
        }

        [HttpGet("password")]
        public async Task<IActionResult> GetPassword([FromQuery] string password)
        {
            var feedback = await _passwordFeedbackService.GetPasswordAsync(password);
            if (feedback != null)
                return Ok(feedback);
            return NotFound("Отзыв с таким паролем не найден");
        }
    }
} 
