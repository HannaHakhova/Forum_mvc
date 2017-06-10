using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FORUM.WEB.Models
{
    public class TopicModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter topic title")]
        [Display(Name = "Topic title")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Please enter description")]
        [Display(Name = "Topic description")]
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public int ForumId { get; set; }
        public int PostCount { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public virtual IEnumerable<PostModel> Posts { get; set; }
    }
}