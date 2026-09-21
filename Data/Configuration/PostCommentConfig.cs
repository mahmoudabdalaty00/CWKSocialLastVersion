using Domain.Models.Posts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Data.Configuration
{
    internal class PostCommentConfig : IEntityTypeConfiguration<PostComment>
    {
        public void Configure(EntityTypeBuilder<PostComment> builder)
        {
            builder.HasKey(pc => pc.Id);

            // Comment -> Post
            builder.HasOne(pc => pc.Post)
                .WithMany(p => p.PostComments)
                .HasForeignKey(pc => pc.PostId)
                .OnDelete(DeleteBehavior.Cascade);

            // Created By
            builder.HasOne(pc => pc.CreatedBy)
                .WithMany(u => u.PostComments)
                .HasForeignKey(pc => pc.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

            // Updated By
            builder.HasOne(pc => pc.UpdatedBy)
                .WithMany()
                .HasForeignKey(pc => pc.UpdatedById)
                .OnDelete(DeleteBehavior.Restrict);

            // Deleted By
            builder.HasOne(pc => pc.DeletedBy)
                .WithMany()
                .HasForeignKey(pc => pc.DeletedById)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
