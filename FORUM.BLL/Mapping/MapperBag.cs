using FORUM.BLL.DTO;
using FORUM.DAL.Entities;

namespace FORUM.BLL.Mapping
{
    public static class MapperBag
    {
        public static MapperWrapper<Forum, ForumDTO> ForumMapper
        {
            get
            {
                return new MapperWrapper<Forum, ForumDTO>();
            }
        }

        public static MapperWrapper<Topic, TopicDTO> TopicMapper
        {
            get
            {
                return new MapperWrapper<Topic, TopicDTO>();
            }
        }
      
        public static MapperWrapper<Post, PostDTO> PostMapper
        {
            get
            {
                return new MapperWrapper<Post, PostDTO>();
            }
        }

        public static MapperWrapper<User, UserDTO> UserMapper
        {
            get
            {
                return new MapperWrapper<User, UserDTO>();
            }
        }

        public static MapperWrapper<UserProfile, UserProfileDTO> UserProfileMapper
        {
            get
            {
                return new MapperWrapper<UserProfile, UserProfileDTO>();
            }
        }
    }
}
