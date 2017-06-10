using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FORUM.BLL.DTO;
using FORUM.BLL.Infrastructure;
using FORUM.BLL.Interfaces;
using FORUM.DAL.Interfaces;
using FORUM.BLL.Mapping;

namespace FORUM.BLL.Services
{

    /// <summary>
    /// Contains methods for getting, creating forums. 
    /// </summary>
    public class ForumService : IForumService
    {
       IUnitOfWork unitOfWork;


        public ForumService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public ForumDTO GetById(int forumId)
        {
            try
            {
                return MapperBag.ForumMapper.Map(unitOfWork.ForumRepository.Get(forumId));
            }
            catch
            {
                return null;
            }
        }

        public OperationDetails Create(ForumDTO forumDto)
        {
            if (forumDto == null)
                return new OperationDetails(false, "Empty subforum has been provided", "");
            if (forumDto.Name == null || forumDto.Name == string.Empty)
                return new OperationDetails(false, "Subforum name required", "Name");
            try
            {
                var forum= MapperBag.ForumMapper.Map(forumDto);
                if (unitOfWork.ForumRepository.Find(f => f.Name == forum.Name).FirstOrDefault() != null)
                    return new OperationDetails(false,"Subforum with the given name already exists", "Name");
                unitOfWork.ForumRepository.Create(forum);
                unitOfWork.Save();
                return new OperationDetails(true, "Subforum was successfully created", "");
            }
            catch (Exception ex)
            {
                return new OperationDetails(false, ex.Message, "");
            }
        }

        public IEnumerable<ForumDTO> GetAll()
        {
            try
            {
                return MapperBag.ForumMapper.Map(unitOfWork.ForumRepository.GetAll());
            }
            catch
            {
                return null;
            }
        }

        public OperationDetails UpdateForum(ForumDTO forumDto)
        {
            try
            {
                if (forumDto == null)
                    return new OperationDetails(false, "Empty forum has been provided", "");

                unitOfWork.ForumRepository.Update(MapperBag.ForumMapper.Map(forumDto));
                unitOfWork.Save();
                return new OperationDetails(true, "Forum successfully updated", "");
            }
            catch (Exception ex)
            {
                return new OperationDetails(false, ex.Message, "");
            }
        }

        public async Task<OperationDetails> Delete(ForumDTO forumDto)
        {
            try
            {
                if (forumDto == null)
                {
                    return new OperationDetails(false, "Empty subforum has been provided", "");
                }
           
                string forumName = forumDto.Name;
                if (forumName == null || forumName == string.Empty)
                {
                    return new OperationDetails(false, "Empty subforum name has been provided", "Name");
                }
                unitOfWork.ForumRepository.Delete(forumDto.Id);
                await unitOfWork.SaveAsync();
                return new OperationDetails(true, "Forum was successfully deleted", forumName);
            }
            catch (Exception ex)
            {
                return new OperationDetails(false, ex.Message, "");
            }
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }
    }
}
