using Domain.Models.Posts;
using Domain.Models.UserProfiles;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data.MainDb
{
    public class DataContext : IdentityDbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {

        }






        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostComment> PostComments { get; set; }
        public DbSet<PostInterAction> PostInterActions { get; set; }



        /// <summary>
        /// Configures the database schema, relationships, and constraints.
        /// </summary>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Important: Call the base method first to ensure Identity tables are configured correctly!
            base.OnModelCreating(builder);

            // AUTOMATION: This scans the current assembly for any class implementing IEntityTypeConfiguration.
            // This is the cleanest way—you don't need to add new configurations here manually anymore.
            builder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);

        }

    }
}
