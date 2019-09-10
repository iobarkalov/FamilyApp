using Cost.Api.Data.Entities;

namespace Cost.Api.Services.Interfaces
{
    public interface IAccountService
    {
        // Авторизация
        User Authenticate(string login, string password);
        // Регистрация
        User Registration(User user, string password);
        // Генерация токена
        string GenerateToken(int userId);
        // TODO придумать другой способ проверки авторизации
        User GetUserById(int userId);
    }
}
