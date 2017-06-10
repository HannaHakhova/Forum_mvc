using FORUM.DAL.Interfaces;
using FORUM.BLL.DTO;
using FORUM.DAL.Entities;

namespace FORUM.BLL.Mapping
{
    public class ComplexMapper
    {
        IUnitOfWork unit;

        public ComplexMapper(IUnitOfWork unitOfWork)
        {
            unit = unitOfWork;
        }

        public Topic Map(TopicDTO topicDto)
        {
            var result = MapperBag.TopicMapper.Map(topicDto);
            result.User = unit.UserManager.FindByNameAsync(topicDto.UserName).Result;
            result.Forum = unit.ForumRepository.Get(topicDto.ForumId);
            return result;
        }

        public Post Map(PostDTO postDto)
        {
            var result = MapperBag.PostMapper.Map(postDto);
            result.User = unit.UserManager.FindByNameAsync(postDto.UserName).Result;
            result.Topic = unit.TopicRepository.Get(postDto.TopicId);
            return result;
        }

    }
}
