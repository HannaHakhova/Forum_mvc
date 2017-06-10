using FORUM.DAL.EF;
using FORUM.DAL.Entities;
using FORUM.DAL.Repositories.General;

namespace FORUM.DAL.Repositories
{
    public class ForumRepository : BaseRepository<Forum>
    {
        public ForumRepository(ForumDbContext dbContext) : base(dbContext)
        {
        }
    }
}
