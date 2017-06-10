using System.Collections.Generic;

namespace FORUM.BLL.DTO
{
    public class ForumDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int TopicCount { get; set; }
        public int PostCount { get; set; }
        public virtual List<TopicDTO> TopicDtos { get; set; }
    }
}
