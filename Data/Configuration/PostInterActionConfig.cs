using Domain.Models.Posts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configuration
{
    internal class PostInterActionConfig : IEntityTypeConfiguration<PostInterAction>
    {
        public void Configure(EntityTypeBuilder<PostInterAction> builder)
        {
            builder.HasKey(pc => pc.Id);
        }
    }
}
