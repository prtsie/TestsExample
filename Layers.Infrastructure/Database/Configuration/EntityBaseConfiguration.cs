using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestsExample.Models;

namespace Layers.Infrastructure.Database.Configuration;

public class EntityBaseConfiguration : IEntityTypeConfiguration<EntityBase>
{
    public void Configure(EntityTypeBuilder<EntityBase> builder)
    {
        builder.UseTpcMappingStrategy();
    }
}