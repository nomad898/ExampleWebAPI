using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Example.Database.EF.Models;

namespace Example.Database.EF.Context.Configurations.Entities
{
    public class UserStateEntityTypeConfiguration : IEntityTypeConfiguration<UserState>
    {
        public void Configure(EntityTypeBuilder<UserState> builder)
        {
            builder.ToTable("UserState");
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
                .WithOne(_ => _.UserState)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasData(
                    new UserState
                    {
                        Id = 1,
                        Code = "Active",
                        Description = "Active State"
                    },
                    new UserState
                    {
                        Id = 2,
                        Code = "Blocked",
                        Description = "Blocked State"
                    }
                );
        }
    }
}
