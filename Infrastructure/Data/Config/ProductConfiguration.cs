using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Core.Entities;

namespace Infrastructure.Data.Config
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            #region Configure columns data type and constraints
            builder.Property(p => p.Id).IsRequired();
            builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Description).IsRequired().HasMaxLength(220);
            builder.Property(p => p.Price).HasColumnType("decimal(18,2)");
            builder.Property(p => p.PictureUrl).IsRequired();
            #endregion

            #region Configure relationship of product table with other tables
            builder.HasOne(pb => pb.ProductBrand).WithMany()
            .HasForeignKey(pb => pb.ProductBrandId);
            builder.HasOne(pt => pt.ProductType).WithMany()
            .HasForeignKey(pt => pt.ProductTypeId);
            #endregion
        }
    }
}