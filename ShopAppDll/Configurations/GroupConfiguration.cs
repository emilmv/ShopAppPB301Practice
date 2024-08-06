using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopAppPB301Practice.Entities;

namespace ShopAppDll.Configurations
{
    public class GroupConfiguration : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder
                .Property(g => g.Name)
                .IsRequired()
                .HasMaxLength(255);
            builder
                .Property(g => g.Limit)
                .IsRequired();
            builder
                .Property(g => g.CreateDate)
                .IsRequired()
                .HasDefaultValueSql("getdate()");
            builder
                .Property(g => g.UpdateDate)
                .HasDefaultValueSql("getdate()");
            builder.
                Property(g => g.Image)
                .IsRequired()
                .HasMaxLength(255);
        }
    }
}
