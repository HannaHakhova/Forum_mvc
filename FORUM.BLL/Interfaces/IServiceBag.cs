using System;

namespace FORUM.BLL.Interfaces
{

    /// <summary>
    /// Contains properties for getting instances of services. 
    /// </summary>
    public interface IServiceBag : IDisposable
    {
        IForumService ForumService { get; }
        ITopicService TopicService { get; }
        IPostService PostService { get; }
        IUserService UserService { get; }
        IUserProfileService UserProfileService { get; }
    }
}
