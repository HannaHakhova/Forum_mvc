using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FORUM.DAL.Entities
{
    public class User : IdentityUser
    {
        public virtual UserProfile UserProfile { get; set; }
        public virtual List<Post> Posts { get; set; }
        public virtual List<Topic> Topics { get; set; }
    }
}
