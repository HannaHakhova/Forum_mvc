using System;
using System.Collections.Generic;

namespace FORUM.BLL.DTO
{
    public class TopicDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? CreationDate { get; set; }
        public int ForumId { get; set; }
        public int UserId { get; set; }
        public int PostCount { get; set; }
        public string UserName { get; set; }
        public virtual IEnumerable<PostDTO> PostDtos { get; set; }

    }
}
