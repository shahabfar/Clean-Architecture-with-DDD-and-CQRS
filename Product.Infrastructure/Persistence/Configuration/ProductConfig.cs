using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Product.Infrastructure.Persistence.Configuration
{
    public class ProductConfig : IEntityTypeConfiguration<Domain.Entities.Product>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Product> builder)
        {
            builder.Property(b => b.Name).HasMaxLength(64);
            builder.Ignore(c => c.DomainEvents);
            //builder.Property(x => x.CreatedBy).HasColumnType("uuid").HasDefaultValueSql("uuid_generate_v4()");
            //builder.Property(x => x.LastModifiedBy).HasColumnType("uuid").HasDefaultValueSql("uuid_generate_v4()");
            //builder.Property(x => x.DeletedBy).HasColumnType("uuid").HasDefaultValueSql("uuid_generate_v4()");
        }
    }
}
