using Domain.Models.Posts;
using Domain.Models.UserProfiles;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data.MainDb;

public class DataContext : IdentityDbContext
{
    public DataContext()
    {
    }

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





    public class WriteDbContext : DbContext
    {
        public WriteDbContext(DbContextOptions<WriteDbContext> options) : base(options)
        {
        }

        public WriteDbContext()
        {
        }
    }

    public class ReadDBContext : DbContext
    {
        public ReadDBContext()
        {
        }

        public ReadDBContext(DbContextOptions<ReadDBContext> options) : base(options)
        {
        }

        [Obsolete("This context is read-only", true)]
        public new int SaveChanges()
        {
            throw new InvalidOperationException("This context is read-only.");
        }

        [Obsolete("This context is read-only", true)]
        public new int SaveChanges(bool acceptAll)
        {
            throw new InvalidOperationException("This context is read-only.");
        }

        [Obsolete("This context is read-only", true)]
        public new Task<int> SaveChangesAsync(CancellationToken token = default)
        {
            throw new InvalidOperationException("This context is read-only.");
        }

        [Obsolete("This context is read-only", true)]
        public new Task<int> SaveChangesAsync(bool acceptAll, CancellationToken token = default)
        {
            throw new InvalidOperationException("This context is read-only.");
        }
    }
}
public class WriteDbContext : DbContext
{
    public WriteDbContext(DbContextOptions<WriteDbContext> options) : base(options)
    {
    }

    public WriteDbContext()
    {
    }
}

public class ReadDBContext : DbContext
{
    public ReadDBContext()
    {
    }

    public ReadDBContext(DbContextOptions<ReadDBContext> options) : base(options)
    {
    }

    [Obsolete("This context is read-only", true)]
    public new int SaveChanges()
    {
        throw new InvalidOperationException("This context is read-only.");
    }

    [Obsolete("This context is read-only", true)]
    public new int SaveChanges(bool acceptAll)
    {
        throw new InvalidOperationException("This context is read-only.");
    }

    [Obsolete("This context is read-only", true)]
    public new Task<int> SaveChangesAsync(CancellationToken token = default)
    {
        throw new InvalidOperationException("This context is read-only.");
    }

    [Obsolete("This context is read-only", true)]
    public new Task<int> SaveChangesAsync(bool acceptAll, CancellationToken token = default)
    {
        throw new InvalidOperationException("This context is read-only.");
    }
}