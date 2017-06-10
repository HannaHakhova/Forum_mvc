using System.Data.Entity;
using FORUM.DAL.Entities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FORUM.DAL.EF
{
    public class ForumDbContext : IdentityDbContext<User>
    {
        public ForumDbContext() : base("ForumConnection")
        {
            Database.SetInitializer(new DbInitializer());
        }

       public DbSet<Topic> Topics { get; set; }
       public DbSet<Forum> Forums { get; set; }
       public DbSet<Post> Posts { get; set; }
       public DbSet<UserProfile> UserProfiles { get; set; }
       
    }
}
