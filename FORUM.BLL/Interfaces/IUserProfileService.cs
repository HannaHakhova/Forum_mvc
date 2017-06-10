using System.Threading.Tasks;
using FORUM.BLL.DTO;
using FORUM.BLL.Infrastructure;

namespace FORUM.BLL.Interfaces
{

    /// <summary>
    /// Contains methods for getting and adding/updating user profiles. 
    /// </summary>
    public interface IUserProfileService
    {
        Task<OperationDetails> UpdateUserProfileAsync(UserProfileDTO userProfileDto);
        Task<UserProfileDTO> FindByNameAsync(string userName);
    }
}
