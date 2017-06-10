using System;

namespace FORUM.DAL.Entities
{
    public class Post : Entity
    {
        public string Message { get; set; }
        public DateTime? PostTime { get; set; }
        public virtual Topic Topic { get; set; }
        public int TopicId { get; set; }
        public virtual User User { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }


    }
}
