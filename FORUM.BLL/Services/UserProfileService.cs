using FORUM.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FORUM.BLL.DTO;
using FORUM.BLL.Infrastructure;
using FORUM.BLL.Mapping;
using FORUM.DAL.Interfaces;

namespace FORUM.BLL.Services
{

    /// <summary>
    /// Contains methods for getting and adding/updating user profiles. 
    /// </summary>
    public class UserProfileService : IUserProfileService
    {
        IUnitOfWork unitOfWork;

        public UserProfileService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        /// <summary>
        /// Sets the given user profile to it's user. If user already has user profile - overrides it.
        /// </summary>
        public async Task<OperationDetails> UpdateUserProfileAsync(UserProfileDTO profile)
        {
            try
            {
                if (profile == null)
                    return new OperationDetails(false, "Empty profile has been provided", "");

                string userName = profile.UserName;
                if (userName == null || userName == string.Empty)
                    return new OperationDetails(false, "Empty user name has been provided", "UserName");

                var user = await unitOfWork.UserManager.FindByNameAsync(userName);
                if (user == null)
                {
                    return new OperationDetails(false, "User does not exist", "UserName");
                }
                if (user.UserProfile != null)
                {
                    unitOfWork.UserProfileRepository.Delete(user.UserProfile.Id);
                    user.UserProfile = null;
                    await unitOfWork.UserManager.UpdateAsync(user);
                }
                var userProfile = MapperBag.UserProfileMapper.Map(profile);
                userProfile.User = user;
                unitOfWork.UserProfileRepository.Create(userProfile);
                unitOfWork.Save();
                return new OperationDetails(true, "User profile successfully updated", "");
            }
            catch (Exception ex)
            {
                return new OperationDetails(false, ex.Message, "");
            }
        }

        public async Task<UserProfileDTO> FindByNameAsync(string userName)
        {
            if (userName == null || userName == string.Empty)
                return null;
            try
            {
                var user = await unitOfWork.UserManager.FindByNameAsync(userName);
                if (user == null)
                {
                    return null;
                }
                var userProfile = user.UserProfile;
             
                if (userProfile == null)
                {
                    return null;
                }
                return MapperBag.UserProfileMapper.Map(userProfile);
            }
            catch
            {
                return null;
            }
        }
    }
}
