using FORUM.BLL.Infrastructure;
using System.Threading.Tasks;
using FORUM.BLL.DTO;
using System.Collections.Generic;

namespace FORUM.BLL.Interfaces
{

    /// <summary>
    /// Contains methods for getting, creating and deleting threads. 
    /// </summary>
    public interface ITopicService
    {
        OperationDetails Create(TopicDTO threadDto);
        Task<OperationDetails> Delete(TopicDTO threadDto);
        IEnumerable<TopicDTO> GetAllTopicsByForum(int forumId);
        IEnumerable<TopicDTO> GetAllTopicsByAuthor(string userName);
        IEnumerable<TopicDTO> GetAll();
        TopicDTO GetById(int topicId);
        void Dispose();
    }
}
