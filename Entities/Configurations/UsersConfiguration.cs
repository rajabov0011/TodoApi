using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TodoApi.Entities.Configurations;
public class UsersConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Username)
            .IsRequired()
            .HasMaxLength(50);
        builder.HasIndex(u => u.Username)
            .IsUnique();
        builder.Property(u => u.PasswordHash)
            .IsRequired()
            .HasMaxLength(255);
    }
}