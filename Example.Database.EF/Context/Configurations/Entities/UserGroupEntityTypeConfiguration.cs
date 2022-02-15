using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Example.Database.EF.Models;

namespace Example.Database.EF.Context.Configurations.Entities
{
    internal class UserGroupEntityTypeConfiguration : IEntityTypeConfiguration<UserGroup>
    {
        public void Configure(EntityTypeBuilder<UserGroup> builder)
        {
            builder.ToTable("UserGroup");
            builder.HasKey(_ => new { _.Id });
            builder
                .Property(_ => _.Id)
                .HasColumnName("ID");
            builder
                .Property(_ => _.Code)
                .IsRequired();
            builder.HasIndex(_ => _.Code)
                .IsUnique();
            builder
                .Property(_ => _.Description);
            builder
                .HasMany(_ => _.Users)
                .WithOne(_ => _.UserGroup)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasData(
                    new UserGroup
                    {
                        Id = 1,
                        Code = "Admin",
                        Description = "Admin Role"
                    },
                    new UserGroup
                    {
                        Id = 2,
                        Code = "User",
                        Description = "User Role"
                    }
            );
        }
    }
}
