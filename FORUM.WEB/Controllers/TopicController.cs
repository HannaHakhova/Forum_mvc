using FORUM.BLL.Interfaces;
using FORUM.WEB.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using FORUM.WEB.Models;
using FORUM.WEB.Mapping;
using PagedList;

namespace FORUM.WEB.Controllers
{
    public class TopicController : Controller
    {
        IServiceBag _serviceBag;


        public TopicController(IServiceBag serviceBag)
        {
            _serviceBag = serviceBag;

        }

        public ActionResult Index()
        {
            var topics = MapperBag.TopicMapper.Map(_serviceBag.TopicService.GetAll());
            var latestTopics = topics.OrderByDescending(t=>t.CreationDate);
            foreach (var item in latestTopics)
            {
                var posts = MapperBag.PostMapper.Map(_serviceBag.PostService.GetPostsOfTopic(item.Id));
                item.PostCount = posts.Count();
            }
          
            ViewBag.LatestTopics = latestTopics;
            if (Request.IsAuthenticated)
            {
                string login = User.Identity.Name;
                var userTopics = MapperBag.TopicMapper.Map(_serviceBag.TopicService.GetAllTopicsByAuthor(login));
                int count = userTopics.Count();
                if (count == 0)
                {
                    ViewBag.EmptyListOfTopics = "I do not have any topics yet.";
                }
                foreach (var item in userTopics)
                {
                    var posts = MapperBag.PostMapper.Map(_serviceBag.PostService.GetPostsOfTopic(item.Id));
                    item.PostCount = posts.Count();
                }
                ViewBag.UserTopics = userTopics;
            }
            return View();
        }

        [HttpGet]
        public ActionResult TopicsByForum(int forumId, int? page)
        {
            try
            {
                var forum = MapperBag.ForumMapper.Map(_serviceBag.ForumService.GetById(forumId));
                ViewBag.Forum = forum.Name;
                var topics = MapperBag.TopicMapper.Map(_serviceBag.TopicService.GetAllTopicsByForum(forumId));
                foreach (var item in topics)
                {
                    var posts = MapperBag.PostMapper.Map(_serviceBag.PostService.GetPostsOfTopic(item.Id));
                    item.PostCount = posts.Count();
                }
                int count = topics.Count();
                if (count == 0)
                {
                    ViewBag.EmptyListOfTopics = "There are no topics yet.";
                }
                var model = topics.ToPagedList(page ?? 1, 10);
                return View(model);
            }
            catch
            {
                return new HttpNotFoundResult();
            }
            
        }


        // GET: Forum/Create
        public ActionResult Create()
        {
            ViewBag.Forums = MapperBag.ForumMapper.Map(_serviceBag.ForumService.GetAll());
            return View();
        }

        // POST: Topic/Create
        [HttpPost]
        public ActionResult Create(TopicModel model)
        {

            if (ModelState.IsValid)
            {
                model.UserName= User.Identity.Name;
                model.CreationDate = DateTime.Now;
                model.PostCount = 0;
                var topic = MapperBag.TopicMapper.Map(model);
               
                var operationDetails = _serviceBag.TopicService.Create(topic);
                if (operationDetails.Succeeded)
                {
                    ViewBag.Item = "Topic";
                    ViewBag.State = "created";
                    return View("Successful");
                }
                else
                {
                    ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
                }
            }
            ViewBag.Forums = MapperBag.ForumMapper.Map(_serviceBag.ForumService.GetAll());
            return View(model);
        }

        
        [HttpGet]
        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult DeleteTopic(int topicId)
        {
            try
            {
                var model = MapperBag.TopicMapper.Map(_serviceBag.TopicService.GetById(topicId));
                var posts = MapperBag.PostMapper.Map(_serviceBag.PostService.GetPostsOfTopic(topicId));
                model.PostCount = posts.Count();
                int count = 0;
                return View(model);
            }
            catch
            {
                return new HttpNotFoundResult();
            }

        }

        
        [HttpPost, ActionName("DeleteTopic")]
        [Authorize(Roles = "Administrator, Moderator")]
        public async Task<ActionResult> DeleteConfirmed(int topicId)
        {
            try
            {
                var topic = _serviceBag.TopicService.GetById(topicId);
                var operationDetails = await _serviceBag.TopicService.Delete(topic);
                if (operationDetails.Succeeded)
                    return Redirect("/Topic/Index");
                ViewBag.Message = operationDetails.Message;
                return View("Index");
            }
            catch
            {
                return HttpNotFound();
            }
            
        }

        public ActionResult TopicPosts(int id)
        {
            try
            {
                var topic = MapperBag.TopicMapper.Map(_serviceBag.TopicService.GetById(id));
                IEnumerable<PostModel> posts = MapperBag.PostMapper.Map(_serviceBag.PostService.GetPostsOfTopic(id));
                TopicPostsModel result = new TopicPostsModel(posts);
                result.Id = topic.Id;
                result.CreationDate = topic.CreationDate;
                result.UserName = topic.UserName;
                result.Posts = posts;
                ViewBag.Avatar = "image.png";
                result.Description = topic.Description;
                result.TopicName = topic.Title;

                return View(result);
            }
            catch
            {
                return new HttpNotFoundResult();
            }
           
        }
    }
}
    
