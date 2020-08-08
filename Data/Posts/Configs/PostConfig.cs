using Entities.Posts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Posts.Configs
{

    public class PostConfig : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.Property(p => p.Title).IsRequired(true).HasMaxLength(200);
            builder.Property(p => p.Description).IsRequired(true).HasMaxLength(200);
            builder.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

            builder.HasOne(p => p.Category)
                .WithMany(p => p.Posts)
                .HasForeignKey(p => p.CategoryId);

            builder.HasOne(p => p.Author)
                .WithMany(p => p.Posts)
                .HasForeignKey(p => p.AuthorId);
        }
    }
}