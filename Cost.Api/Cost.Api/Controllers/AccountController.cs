using AutoMapper;
using Cost.Api.Data.Entities;
using Cost.Api.Helpers;
using Cost.Api.Models;
using Cost.Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cost.Api.Controllers
{
    /// <summary>
    /// Контроллер для авторизации/регистрации пользователей
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public AccountController(IAccountService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }

        /// <summary>
        /// Авторизация в приложении
        /// </summary>
        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody]UserDto userDto)
        {
            var user = _accountService.Authenticate(userDto.Email, userDto.Password);

            if (user == null)
                return BadRequest(new {message = "Email или пароль указаны неверно"});

            var tokenString = _accountService.GenerateToken(user.Id);

            // TODO Переписать на модель
            return Ok(new
            {
                user.Id,
                user.Name,
                user.Email,
                Token = tokenString
            });
        }

        /// <summary>
        /// Регистрация в приложении
        /// </summary>
        [AllowAnonymous]
        [HttpPost]
        [Route("registration")]
        public IActionResult Registration([FromBody]UserDto userDto)
        {
            // Преобразуем запрос в модель пользователя
            // (не используем модели запросов/ответов внутри приложения)
            var user = _mapper.Map<User>(userDto);

            try
            {
                _accountService.Registration(user, userDto.Password);
                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        [Route("secret")]
        public IActionResult Secret()
        {
            var s = User;

            return Ok();
        }
    }
}
