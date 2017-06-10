using System.Collections.Generic;
using System.Threading.Tasks;
using FORUM.BLL.DTO;
using FORUM.BLL.Infrastructure;

namespace FORUM.BLL.Interfaces
{

    /// <summary>
    /// Contains methods for getting, creating forums. 
    /// </summary>
    public interface IForumService
    {
        IEnumerable<ForumDTO> GetAll();
        ForumDTO GetById(int id);
        OperationDetails Create(ForumDTO forumDto);
        Task<OperationDetails> Delete(ForumDTO forumDto);
        OperationDetails UpdateForum(ForumDTO forumDto);
        void Dispose();

    }
}
