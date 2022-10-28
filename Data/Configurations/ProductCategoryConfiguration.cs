
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Configurations
{
    public class ProductCategoryConfiguration : IEntityTypeConfiguration<ProductCategory>
    {
        
        public void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            //categoryId and ProductId becomes keys of ProductCategory table.
            builder.HasKey(pc => new { pc.CategoryId, pc.ProductId });
        }
    }
}
