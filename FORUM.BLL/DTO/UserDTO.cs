using System;
using System.Collections.Generic;

namespace FORUM.BLL.DTO
{
    public class UserDTO
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
        public IList<string> Roles { get; set; }
        public int UserProfileId { get; set; }
        public virtual List<PostDTO> PostDtos { get; set; }
        public virtual List<TopicDTO> ThreadDtos { get; set; }
        public string Avatar { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}
