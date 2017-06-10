using FORUM.DAL.Entities;
using FORUM.DAL.Repositories.General;
using FORUM.DAL.EF;

namespace FORUM.DAL.Repositories
{
    public class PostRepository : BaseRepository<Post>
    {
        public PostRepository(ForumDbContext dbContext) : base(dbContext)
        {
        }
    }
}
