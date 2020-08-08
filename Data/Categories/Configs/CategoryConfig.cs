using Entities.Categories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Categories.Configs
{

    public class CategoryConfig : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(c => c.Name).IsRequired(true).HasMaxLength(200);


            builder.HasOne(c => c.ParentCategory).WithMany().HasForeignKey(c => c.ParentCategoryId);

            builder.HasMany(c => c.Posts).WithOne(c => c.Category)
                .HasForeignKey(c => c.CategoryId);

            builder.HasMany(c => c.ChildCategories).WithOne()
                .HasForeignKey(c => c.ParentCategoryId);

        }
    }
}