using Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Users.Configs
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(u => u.UserName).IsRequired(true).HasMaxLength(500);
            builder.Property(u => u.PasswordHash).IsRequired(true).HasMaxLength(200);
            builder.Property(u => u.FullName).IsRequired(true).HasMaxLength(200);
            builder.Property(u => u.Age).IsRequired(true);
            builder.Property(u => u.Gender).IsRequired(true);

            builder.HasMany(u => u.Posts).WithOne(u => u.Author)
                .HasForeignKey(u => u.AuthorId);

        }
    }

}