using System.Collections.Generic;

namespace FORUM.DAL.Entities
{
    public class Forum : Entity
    {
        public string Name{ get; set; }
        public string Description { get; set; }
        public int TopicCount { get; set; }
        public int PostCount { get; set; }
        public virtual List<Topic> Topics { get; set; }
     }
}
