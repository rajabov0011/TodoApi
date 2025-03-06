using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TodoApi.Entities.Configurations;
public class TodosConfiguration : IEntityTypeConfiguration<Todo>
{
    public void Configure(EntityTypeBuilder<Todo> builder)
    {
        builder.HasKey(t => t.Id);
        builder.Property(t => t.Title)
            .IsRequired()
            .HasMaxLength(255);
        builder.Property(t => t.Description)
            .HasMaxLength(1000);
        builder.Property(t => t.IsCompleted)
            .IsRequired();
        builder.HasOne(t => t.User)
            .WithMany(u => u.Todos)
            .HasForeignKey(t => t.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}