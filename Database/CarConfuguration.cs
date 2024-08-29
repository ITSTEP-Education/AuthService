using AuthJWTAspNetWeb.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthJWTAspNetWeb.Database
{
    public class CarConfuguration : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder.ToTable("cars").HasKey(c => c.id);
            builder.Property(c => c.number).IsRequired().HasDefaultValue(null).HasMaxLength(8);
            builder.HasIndex(c => c.number).IsUnique().HasDatabaseName("IndexNumber");

            builder.Property(c => c.vinCode).HasDefaultValue("none").HasMaxLength(17); ;
            builder.Property(c => c.model).IsRequired().HasDefaultValue(null).HasMaxLength(8);
            builder.Property(c => c.volume).IsRequired().HasDefaultValue(0);
            builder.Property(c => c.price).IsRequired().HasDefaultValue(0);

            builder.ToTable(c => c.HasCheckConstraint("ValidVolume", "volume > 0 AND volume <= 6"));
            builder.ToTable(c => c.HasCheckConstraint("ValidPrice", "price > 1000 AND price <= 10000"));
        }
    }
}
