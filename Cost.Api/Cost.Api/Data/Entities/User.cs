namespace Cost.Api.Data.Entities
{
    /// <summary>
    /// Модель пользователя
    /// </summary>
    public class User
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Email пользователя
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Закодированный пароль пользователя
        /// </summary>
        public byte[] PasswordHash { get; set; }

        /// <summary>
        /// Соль для декодирования пароля пользователя
        /// </summary>
        public byte[] PasswordSalt { get; set; }
    }
}
