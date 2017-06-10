using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FORUM.DAL.Entities
{
    /// <summary>
    /// A holding class to identify our entity classes generically
    /// </summary>
    public abstract class Entity
    {
        public int Id { get; set; }
    }
}
