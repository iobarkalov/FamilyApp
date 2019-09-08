using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cost.Api.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cost.Api.Data.EntityConfigurations
{
    /// <summary>
    /// Конфигурация таблицы User в базе данных
    /// </summary>
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User", "dbo");

            builder.HasKey(u => u.Id)
                .HasName("PK_User");

            builder.Property(u => u.Id)
                .HasColumnName("Id")
                .ValueGeneratedOnAdd();

            builder.Property(u => u.Name)
                .HasColumnName("Name")
                .HasColumnType("nvarchar(50)")
                .IsRequired();

            builder.Property(u => u.Email)
                .HasColumnName("Email")
                .HasColumnType("nvarchar(50)")
                .IsRequired();

            builder.Property(u => u.Password)
                .HasColumnName("Password")
                .HasColumnType("varchar(100)")
                .IsRequired();
        }
    }
}
