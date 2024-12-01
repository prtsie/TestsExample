using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestsExample.Database.Models;

namespace TestsExample.Database.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(u => u.Name)
            .IsRequired()
            .HasMaxLength(Constants.MaxUsernameLength);

        builder.Property(u => u.Password)
            .IsRequired()
            .HasMaxLength(Constants.MaxPasswordLength);
    }
}