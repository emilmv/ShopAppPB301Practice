using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopAppDll.Entities;

namespace ShopAppDll.Configurations
{
    public class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.Property(a => a.Name)
                .IsRequired()
                .HasMaxLength(20);
            builder.Property(a=>a.Surname)
                .IsRequired()
                .HasMaxLength(20);
        }
    }
}
