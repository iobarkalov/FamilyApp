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
        /// <summary>
        /// Авторизация в приложении
        /// </summary>
        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            return Ok(request);
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
