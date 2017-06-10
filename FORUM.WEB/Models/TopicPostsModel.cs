using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FORUM.WEB.Models
{
    public class TopicPostsModel
    {
        public int Id { get; set; }
        public string TopicName { get; set; }
        public DateTime CreationDate { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Description { get; set; }
        public UserModel TopicCreator { get; set; }
        public IEnumerable<PostModel> Posts { get; set; }
        public int PageNo { get; set; }
        public int CountPage { get; set; }
        public int ItemPerPage { get; set; }
        public TopicPostsModel(IEnumerable<PostModel> list)
        {
            var count = list.Count();
        }

    }
}