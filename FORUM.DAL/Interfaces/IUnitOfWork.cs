using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using FORUM.DAL.Entities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FORUM.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        UserManager<User> UserManager { get; }
        RoleManager<IdentityRole> RoleManager { get; }
        IRepository<UserProfile> UserProfileRepository { get; }
        IRepository<Topic> TopicRepository { get; }
        IRepository<Forum> ForumRepository { get; }
        IRepository<Post> PostRepository { get; }

        Task SaveAsync();
        void Save();
    }
}
