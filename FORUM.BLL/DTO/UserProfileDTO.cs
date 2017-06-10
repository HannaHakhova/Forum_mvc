using System;

namespace FORUM.BLL.DTO
{
    public class UserProfileDTO
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Avatar { get; set; }
        public DateTime RegistrationDate { get; set; }


    }
}
