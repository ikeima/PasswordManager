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
        /// <summary>
        /// Добавляет новую комбинацию пароль-отзыв
        /// </summary>
        /// <param name="combination"></param>
        /// <returns></returns>
        [HttpPost("add")]
        public async Task<IActionResult> AddNewCombination([FromBody] PasswordFeedback combination)
        {
            var result = await _passwordFeedbackService.AddNewCombinationAsync(combination);
            if (result)
                return Ok("Успешно добавлено");
            return BadRequest("Комбинация с таким паролем уже существует");
        }

        /// <summary>
        /// Получеает количество комбинаций
        /// </summary>
        /// <returns></returns>
        [HttpGet("count")]
        public async Task<IActionResult> GetCount()
        {
            var count = await _passwordFeedbackService.GetCountAsync();
            return Ok(count);
        }
        /// <summary>
        /// Полученает хэш пароля по отзыву
        /// </summary>
        /// <param name="feedback"></param>
        /// <returns></returns>
        [HttpGet("hash")]
        public async Task<IActionResult> GetHashByFeedback([FromQuery] string feedback)
        {
            var hash = await _passwordFeedbackService.GetHashByFeedbackAsync(feedback);
            if (hash != null)
                return Ok(hash);
            return NotFound("Пароль с таким отзывом не найден");
        }
        /// <summary>
        /// Получает отзыв по паролю
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpGet("feedback")]
        public async Task<IActionResult> GetFeedbackByPassword([FromQuery] string password)
        {
            var feedback = await _passwordFeedbackService.GetPasswordAsync(password);
            if (feedback != null)
                return Ok(feedback);
            return NotFound("Отзыв с таким паролем не найден");
        }
    }
}
