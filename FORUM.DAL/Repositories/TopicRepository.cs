using FORUM.DAL.Entities;
using FORUM.DAL.EF;
using FORUM.DAL.Repositories.General;

namespace FORUM.DAL.Repositories
{
    public class TopicRepository : BaseRepository<Topic>
    {
        public TopicRepository(ForumDbContext dbContext) : base(dbContext)
        {
        }
    }
}
