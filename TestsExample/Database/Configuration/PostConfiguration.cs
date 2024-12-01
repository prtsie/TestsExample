using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestsExample.Database.Models;

namespace TestsExample.Database.Configuration;

public class PostConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.Property(p => p.Title)
            .IsRequired()
            .HasMaxLength(Constants.MaxPostTitleLength);
        
        builder.Property(p => p.Content)
            .HasMaxLength(Constants.MaxPostTextLength);

        builder.Property(p => p.UserId)
            .IsRequired();

        builder.HasOne<User>().WithMany();
    }
}