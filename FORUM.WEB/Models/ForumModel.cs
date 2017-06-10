using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using FORUM.BLL.DTO;

namespace FORUM.WEB.Models
{
    public class ForumModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter subforum name")]
        [MaxLength(15, ErrorMessage = "Max lenght 15 characters exceeded")]
        [Display(Name = "Subforum name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter description")]
        [MaxLength(100, ErrorMessage = "Max lenght 100 characters exceeded" )]
        public string Description { get; set; }
        public int TopicCount { get; set; }
        public int PostCount { get; set; }
        public IEnumerable<TopicModel> Topics { get; set; }
        public int UserId { get; set; }
        public ForumModel Forum { get; set; }
     }
}