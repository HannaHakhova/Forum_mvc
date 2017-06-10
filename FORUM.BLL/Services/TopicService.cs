using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FORUM.BLL.DTO;
using FORUM.BLL.Infrastructure;
using FORUM.BLL.Interfaces;
using FORUM.BLL.Mapping;
using FORUM.DAL.Interfaces;
using FORUM.DAL.Entities;

namespace FORUM.BLL.Services
{
    /// <summary>
    /// Contains methods for getting, creating and deleting threads. 
    /// </summary>
    public class TopicService : ITopicService
    {
        IUnitOfWork _unitOfWork;
        ComplexMapper mapper;

        public TopicService(IUnitOfWork unitOfWork)
        { 
            _unitOfWork = unitOfWork;
            mapper = new ComplexMapper(unitOfWork);
        }

        public OperationDetails Create(TopicDTO topicDto)
        {
            if (topicDto == null)
                return new OperationDetails(false, "Empty topic has been provided", "");
            if (topicDto.Title == null || topicDto.Title == string.Empty)
                return new OperationDetails(false, "Topic title required", "Title");
            if (topicDto.Description == null || topicDto.Description == string.Empty)
                return new OperationDetails(false, "Topic description required", "Description");
            if (topicDto.ForumId == 0)
                return new OperationDetails(false, "Subforum is required", "ForumId");
            try
            {
                var topic = MapperBag.TopicMapper.Map(topicDto);
                if (_unitOfWork.TopicRepository.Find(t => t.Title == topicDto.Title).FirstOrDefault() != null)
                    return new OperationDetails(false, "Topic with the given title already exists", "Title");
                _unitOfWork.TopicRepository.Create(topic);
               _unitOfWork.Save();
                return new OperationDetails(true, "Topic was successfully created", "");
            }
            catch (Exception ex)
            {
                return new OperationDetails(false, ex.Message, "");
            }
        }

      

        public async Task<OperationDetails> Delete(TopicDTO topicDto)
        {
            try
            {
                if (topicDto == null)
                {
                    return new OperationDetails(false, "Empty topic has been provided", "");
                }

                string topicTitle = topicDto.Title;
                if (topicTitle == null || topicTitle == string.Empty)
                {
                    return new OperationDetails(false, "Empty topic title has been provided", "Title");
                }
                _unitOfWork.TopicRepository.Delete(topicDto.Id);
                await _unitOfWork.SaveAsync();
                return new OperationDetails(true, "Topic was successfully deleted", topicTitle);
            }
            catch (Exception ex)
            {
                return new OperationDetails(false, ex.Message, "");
            }
        }

     

        public IEnumerable<TopicDTO> GetAll()
        {
            try
            {
                return MapperBag.TopicMapper.Map(_unitOfWork.TopicRepository.GetAll().ToList());
            }
            catch
            {
                return null;
            }
        }

      public IEnumerable<TopicDTO> GetAllTopicsByForum(int forumId)
        {
            try
            {
                return MapperBag.TopicMapper.Map(_unitOfWork.TopicRepository.Find(t => t.ForumId == forumId).ToList());
            }
            catch
            {
                return null;
            }
        }

      
        public TopicDTO GetById(int topicId)
        {
            try
            {
                return MapperBag.TopicMapper.Map(_unitOfWork.TopicRepository.Get(topicId));
            }
            catch
            {
                return null;
            }
        }
        public IEnumerable<TopicDTO> GetAllTopicsByAuthor(string userName)
        {
            try
            {
                return MapperBag.TopicMapper.Map(_unitOfWork.TopicRepository.Find(t => t.UserName == userName).ToList());
            }
            catch
            {
                return null;
            }
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }

        
}

