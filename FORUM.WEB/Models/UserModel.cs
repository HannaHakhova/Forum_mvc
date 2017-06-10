using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FORUM.WEB.Models
{
    public class UserModel
    {
        public string Id { get; set; }
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter login")]
        [Display(Name = "Login")]
        public string UserName { get; set; }
        public IList<string> Roles { get; set; }
        public string Avatar { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}