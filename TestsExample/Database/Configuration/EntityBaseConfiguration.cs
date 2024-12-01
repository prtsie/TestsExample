using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestsExample.Database.Models;

namespace TestsExample.Database.Configuration;

public class EntityBaseConfiguration : IEntityTypeConfiguration<EntityBase>
{
    public void Configure(EntityTypeBuilder<EntityBase> builder)
    {
        builder.UseTpcMappingStrategy();
    }
}