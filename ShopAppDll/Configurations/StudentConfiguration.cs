
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopAppPB301Practice.Entities;

namespace ShopAppDll.Configurations
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder
                .Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(255);
            builder
                .HasOne(s=>s.Group)
                .WithMany(g=>g.Students)
                .HasForeignKey(s=>s.GroupId)
                .OnDelete(DeleteBehavior.Cascade);
            builder
                .Property(s => s.CreateDate)
                .IsRequired()
                .HasDefaultValueSql("getdate()");
            builder
                .Property(s => s.UpdateDate)
                .HasDefaultValueSql("getdate()");
        }
    }
}
