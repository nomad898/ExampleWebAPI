using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using Example.Database.EF.Models;

namespace Example.Database.EF.Context.Configurations.Entities
{
    internal class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");
            builder.HasKey(_ => new { _.Id });
            builder
                .Property(_ => _.Id)
                .HasColumnName("ID");
            builder
                .Property(_ => _.Login)
                .IsRequired();
            builder.HasIndex(_ => _.Login)
                .IsUnique();
            builder
                .Property(_ => _.Password)
                .IsRequired();
            builder
                .Property(_ => _.UserGroupId)
                .HasColumnName("UserGroupID")
                .IsRequired();
            builder
                .Property(_ => _.UserStateId)
                .HasColumnName("UserStateID")
                .IsRequired();

            builder.HasData(
                new User 
                {
                    Id = 1,
                    Login = "admin",
                    Password = "admin",
                    CreatedDate = DateTime.Now,
                    UserGroupId = 1,
                    UserStateId = 1
                });
        }
    }
}
