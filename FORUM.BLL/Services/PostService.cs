using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FORUM.BLL.DTO;
using FORUM.BLL.Infrastructure;
using FORUM.BLL.Interfaces;
using FORUM.BLL.Mapping;
using FORUM.DAL.Interfaces;

namespace FORUM.BLL.Services
{

    /// <summary>
    /// Contains methods for getting, creating and deleting posts. 
    /// </summary>
    public class PostService : IPostService
    {
        IUnitOfWork _unitOfWork;
        ComplexMapper mapper;

        public PostService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            mapper = new ComplexMapper(unitOfWork);
        }

        public OperationDetails Create(PostDTO postDto)
        {
                if (postDto == null)
                    return new OperationDetails(false, "Empty post has been provided", "");
                if (postDto.Message == null || postDto.Message == string.Empty)
                    return new OperationDetails(false, "Post message required", "Message");
                if (postDto.TopicId == 0)
                    return new OperationDetails(false, "Thread is required", "ThreadId");
                try
                {
                    var post = mapper.Map(postDto);
                    
                    _unitOfWork.PostRepository.Create(post);
                    _unitOfWork.Save();
                    return new OperationDetails(true, "Post was successfully created", "");
                }
                catch (Exception ex)
                {
                    return new OperationDetails(false, ex.Message, "");
                }
         }

        public async Task<OperationDetails> Delete(PostDTO postDto)
        {
            try
            {
                if (postDto == null)
                {
                    return new OperationDetails(false, "Empty post has been provided", "");
                }

                string postMessage = postDto.Message;
                if (postMessage == null || postMessage == string.Empty)
                {
                    return new OperationDetails(false, "Empty post message has been provided", "Message");
                }
                _unitOfWork.PostRepository.Delete(postDto.Id);
                await _unitOfWork.SaveAsync();
                return new OperationDetails(true, "Post was successfully deleted", postMessage);
            }
            catch (Exception ex)
            {
                return new OperationDetails(false, ex.Message, "");
            }
        }
    

      
        public IEnumerable<PostDTO> GetPostsOfTopic(int topicId)
        {
            try
            {
                return MapperBag.PostMapper.Map(_unitOfWork.PostRepository.Find(p => p.TopicId== topicId).ToList());
            }
            catch
            {
                return null;
            }
        }

        public PostDTO GetById(int postId)
        {
            try
            {
                return MapperBag.PostMapper.Map(_unitOfWork.PostRepository.Get(postId));
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
