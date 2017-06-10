using System;
using System.Collections.Generic;

namespace FORUM.DAL.Entities
{
    public class Topic : Entity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? CreationDate { get; set; }
        public virtual Forum Forum { get; set; }
        public int ForumId { get; set; }
        public virtual User User { get; set; }
        public int UserId { get; set; }
        public int PostCount { get; set; }
        public string UserName { get; set; }
        public virtual IEnumerable<Post> Posts { get; set; }
    }
}
