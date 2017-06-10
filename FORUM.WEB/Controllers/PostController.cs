using FORUM.BLL.Interfaces;
using FORUM.WEB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using FORUM.WEB.Models;

namespace FORUM.WEB.Controllers
{
    public class PostController : Controller
    {
        IServiceBag _serviceBag;


        public PostController(IServiceBag serviceBag)
        {
            _serviceBag = serviceBag;

        }

        // GET: Post
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult Create()
        {
            PostModel post = new PostModel();
            string userName = HttpContext.GetOwinContext().Authentication.User.Identity.Name;
            post.UserName = userName;
            post.PostTime = DateTime.Now;
            int topicId = int.Parse(Request.Params["Id"]);
            var topic = MapperBag.TopicMapper.Map(_serviceBag.TopicService.GetById(topicId));
            post.Message = Request.Params["message"];
            post.TopicId = topicId;
            _serviceBag.PostService.Create(MapperBag.PostMapper.Map(post));
            var posts = MapperBag.PostMapper.Map(_serviceBag.PostService.GetPostsOfTopic(topicId));
            topic.Posts = posts;
            return RedirectToAction("TopicPosts", "Topic", topic);
        }

        [HttpGet]
        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult DeletePost(int postId)
        {
            try
            {
                var post = MapperBag.PostMapper.Map(_serviceBag.PostService.GetById(postId));
                return View(post);
            }
            catch
            {
                return new HttpNotFoundResult();
            }

        }


        [HttpPost]
        [Authorize(Roles = "Administrator, Moderator")]
        public async Task<ActionResult> DeleteConfirmed(int postId)
        {

            var post = _serviceBag.PostService.GetById(postId);
            int topicId= post.TopicId;
            var operationDetails = await _serviceBag.PostService.Delete(post);
            if (operationDetails.Succeeded)
                return RedirectToAction("TopicPosts","Topic", new { id = topicId });
            ViewBag.Message = operationDetails.Message;
            return View("Unsuccessful");
        }
    }

}

