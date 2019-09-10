using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Cost.Api.Data.DbContexts;
using Cost.Api.Data.Entities;
using Cost.Api.Helpers;
using Cost.Api.Services.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Cost.Api.Services
{
    public class AccountService : IAccountService
    {
        private readonly FamilyAppDbContext _context;
        private readonly AppSettings _appSettings;

        public AccountService(FamilyAppDbContext context, IOptions<AppSettings> appSettings)
        {
            _context = context;
            _appSettings = appSettings.Value;
        }

        /// <summary>
        /// Авторизации пользователя
        /// </summary>
        /// <param name="email">Email пользователя</param>
        /// <param name="password">Зашифрованный пароль пользователя</param>
        public User Authenticate(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                return null;

            var user = _context.Users.FirstOrDefault(x => x.Email == email);

            // Проверяем существует ли пользователь
            if (user == null)
                return null;

            // Проверяем пароль пользователя
            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            return user;
        }

        /// <summary>
        /// Регистрация нового пользователя
        /// </summary>
        /// <param name="user">Модель пользователя</param>
        /// <param name="password">Пароль пользователя</param>
        public User Registration(User user, string password)
        {
            // Проверяем задан ли пароль
            if (string.IsNullOrEmpty(password))
                throw new AppException("Password is required");

            // Существует ли пользователь с таким Email
            if (_context.Users.Any(x => x.Email == user.Email)) 
                throw new AppException("Email \"" + user.Email + "\" is already taken");

            CreatePasswordHash(password, out var passwordHash, out var passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _context.Users.Add(user);
            _context.SaveChanges();

            return user;
        }

        /// <summary>
        /// Генерация JWT
        /// </summary>
        /// <param name="userId">Id пользователя</param>
        public string GenerateToken(int userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, userId.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }

        /// <summary>
        /// Загружаем пользователя по Id
        /// TODO вынести работу с базой в репозиторий
        /// TODO использовать ассинхронные методы
        /// </summary>
        /// <param name="userId">Id пользователя</param>
        public User GetUserById(int userId)
        {
            return _context.Users.Find(userId);
        }

        /// <summary>
        /// Шифруем пароль пользователя
        /// </summary>
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            // Проверяем задан ли пароль
            if (password == null)
                throw new ArgumentNullException(nameof(password));

            // Проверяем пароль на пустую строку
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Пароль не должен быть пустой строкой!", nameof(password));

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        /// <summary>
        /// Декодируем пароль и проверяем совпадает ли он с переданным
        /// </summary>
        private bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) 
                throw new ArgumentException("password");

            if (string.IsNullOrEmpty(password))
                throw new ArgumentException("Value cannot be empty or whitespace only string.", nameof(password));

            if (storedHash.Length != 64)
                throw new ArgumentException("Invalid length of password hash (64 bytes expected).", nameof(storedHash));

            if (storedSalt.Length != 128)
                throw new ArgumentException("Invalid length of password salt (128 bytes expected).", nameof(storedSalt));

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (var i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i])
                        return false;
                }
            }

            return true;
        }
    }
}
