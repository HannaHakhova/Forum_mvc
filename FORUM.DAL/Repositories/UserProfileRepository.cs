using FORUM.DAL.Entities;
using FORUM.DAL.EF;
using FORUM.DAL.Repositories.General;

namespace FORUM.DAL.Repositories
{
    public class UserProfileRepository : BaseRepository<UserProfile>
    {
        public UserProfileRepository(ForumDbContext dbContext) : base(dbContext)
        {
        }
    }
}
