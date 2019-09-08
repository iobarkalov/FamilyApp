using System.Linq;
using Cost.Api.Data.DbContexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Cost.Api.Models.Request;

namespace Cost.Api.Controllers
{
    /// <summary>
    /// Контроллер для авторизации/регистрации пользователей
    /// </summary>
    [AllowAnonymous]
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        private readonly FamilyAppDbContext _context;

        /// <summary>
        /// Прокидываем контекст для работы с бд
        /// TODO написать сервис для авториации
        /// TODO вынести в сервис по авторизации
        /// </summary>
        public AccountController(FamilyAppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Авторизация в приложении
        /// </summary>
        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var user = _context.Users
                .FirstOrDefault(x => x.Email == request.Login);

            if (user == null)
            {
                return BadRequest(new { message = "User not found" });
            }

            return Ok(user);
        }

        /// <summary>
        /// Регистрация в приложении
        /// </summary>
        [HttpPost]
        [Route("registration")]
        public IActionResult Registration()
        {
            return Ok();
        }
    }
}
