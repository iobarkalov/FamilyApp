using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cost.Api.Models.Request
{
    /// <summary>
    /// Модель входящего запроса авторизации
    /// </summary>
    public class LoginRequest
    {
        /// <summary>
        /// Логин
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        public string Password { get; set; }
    }
}
