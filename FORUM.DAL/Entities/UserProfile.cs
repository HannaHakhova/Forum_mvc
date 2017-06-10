using System;
using System.ComponentModel.DataAnnotations;


namespace FORUM.DAL.Entities
{
    public class UserProfile : Entity 
    {
        [Required]
        public User User { get; set; }

        public string UserName { get; set; }
        public string Email { get; set; }
        public string Avatar { get; set; }
        public DateTime? RegistrationDate { get; set; }




    }
}
