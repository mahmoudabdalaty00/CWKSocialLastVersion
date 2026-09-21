using Domain.Models.Posts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configuration
{
    internal class PostInterActionConfig : IEntityTypeConfiguration<PostInterAction>
    {
        public void Configure(EntityTypeBuilder<PostInterAction> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Post)
                .WithMany(p => p.PostInterActions)
                .HasForeignKey(x => x.PostId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
