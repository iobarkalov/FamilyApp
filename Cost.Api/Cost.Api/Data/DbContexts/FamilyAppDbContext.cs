using Cost.Api.Data.Entities;
using Cost.Api.Data.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Cost.Api.Data.DbContexts
{
    /// <summary>
    /// Контекст для подключения к базе данных
    /// </summary>
    public class FamilyAppDbContext : DbContext
    {
        public FamilyAppDbContext(DbContextOptions<FamilyAppDbContext> options)
            : base(options)
        {
        }

        // Таблица User в базе данных
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Задаем конфигурацию для таблиц базы данных
        /// </summary>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new UserConfiguration());
        }
    }
}
