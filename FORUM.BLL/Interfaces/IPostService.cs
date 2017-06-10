using FORUM.BLL.Infrastructure;
using System.Threading.Tasks;
using FORUM.BLL.DTO;
using System.Collections.Generic;

namespace FORUM.BLL.Interfaces
{

    /// <summary>
    /// Contains methods for getting, creating and deleting posts. 
    /// </summary>
    public interface IPostService
    {
        OperationDetails Create(PostDTO postDto);
        Task<OperationDetails> Delete(PostDTO postDto);
        IEnumerable<PostDTO> GetPostsOfTopic(int topicId);
        PostDTO GetById(int postId);
        void Dispose();
    }
}
