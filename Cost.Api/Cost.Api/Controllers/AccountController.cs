using System.Web.Http;

namespace Cost.Api.Controllers
{
    /// <summary>
    /// Контроллер для авторизации/регистрации пользователей
    /// </summary>
    [Route("[controller]")]
    public class AccountController : ApiController
    {
        /// <summary>
        /// Авторизация в приложении
        /// </summary>
        [HttpPost]
        public IHttpActionResult Login()
        {
            return Ok();
        }

        /// <summary>
        /// Регистрация в приложении
        /// </summary>
        [HttpPost]
        public IHttpActionResult Registration()
        {
            return Ok();
        }
    }
}
