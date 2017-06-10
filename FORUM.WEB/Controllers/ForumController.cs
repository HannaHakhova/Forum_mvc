using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using FORUM.BLL.Interfaces;
using FORUM.WEB.Models;
using FORUM.WEB.Util;
using FORUM.WEB.Mapping;
using PagedList;

namespace FORUM.WEB.Controllers
{
    public class ForumController : Controller
    {
        IServiceBag _serviceBag;
        
        public ForumController(IServiceBag serviceBag)
        {
            _serviceBag = serviceBag;

        }
 
        [HttpGet]
        public ActionResult ForumList(int? page)
        {
            var forums = MapperBag.ForumMapper.Map(_serviceBag.ForumService.GetAll());
            foreach (var item in forums)
            {
                var topics = MapperBag.TopicMapper.Map(_serviceBag.TopicService.GetAllTopicsByForum(item.Id));
                item.TopicCount = topics.Count();
                int count = 0;
                foreach (var t in topics)
                {
                    var posts = MapperBag.PostMapper.Map(_serviceBag.PostService.GetPostsOfTopic(t.Id));
                    count += posts.Count();
                }
                item.PostCount = count;
            }
            var model = forums.ToPagedList(page ?? 1, 10);
            return View(model);
        }

        // GET: Forum/Create
        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Forum/Create
        [HttpPost]
        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult Create(ForumModel model)
        {
            if (ModelState.IsValid)
            {
                model.PostCount = 0;
                model.TopicCount = 0;
                var subforum = MapperBag.ForumMapper.Map(model);
                var operationDetails = _serviceBag.ForumService.Create(subforum);
                if (operationDetails.Succeeded)
                {
                    ViewBag.Item = "Subforum";
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

        // GET: Forum/Edit/5
        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult EditForum(int forumId)
        {

            try
            {
                var model = MapperBag.ForumMapper.Map(_serviceBag.ForumService.GetById(forumId));
                return View(model);
            }
            catch
            {
                return new HttpNotFoundResult();
            }
        }

        [HttpPost]
        [Authorize(Roles = "Administrator, Moderator")]
        public async Task<ActionResult> Edit(ForumModel model)
        {
           
                var subforum = MapperBag.ForumMapper.Map(model);
                var operationDetails = _serviceBag.ForumService.UpdateForum(subforum);
                if (operationDetails.Succeeded)
                {
                    ViewBag.Item = "Subforum";
                    ViewBag.State = "updated";
                    return View("Successful");
                }
                else
                {
                    ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
                }
            return View("ForumList");

        }

        // GET: Forum/Delete
        [HttpGet]
        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult DeleteForum(int forumId)
        {
            try
            {
                var model = MapperBag.ForumMapper.Map(_serviceBag.ForumService.GetById(forumId));
                var topics = MapperBag.TopicMapper.Map(_serviceBag.TopicService.GetAllTopicsByForum(forumId));
                model.TopicCount = topics.Count();
                int count = 0;
                foreach (var t in topics)
                {
                    var posts = MapperBag.PostMapper.Map(_serviceBag.PostService.GetPostsOfTopic(t.Id));
                    count += posts.Count();
                }
                model.PostCount = count;
                return View(model);
            }
            catch
            {
                return new HttpNotFoundResult();
            }
            
        }

        // POST: Forum/Delete
        [HttpPost, ActionName("DeleteSubforum")]
        [Authorize(Roles = "Administrator, Moderator")]
        public async Task<ActionResult> DeleteConfirmed(int forumId)
        {

                   var forum = _serviceBag.ForumService.GetById(forumId);
                   var operationDetails = await _serviceBag.ForumService.Delete(forum);
                   if (operationDetails.Succeeded)
                        return Redirect("/Forum/ForumList");
                   ViewBag.Message = operationDetails.Message;
                   return View("Unsuccessfull");
         }



        protected override void Dispose(bool disposing)
        {
            _serviceBag.Dispose();
            base.Dispose(disposing);
        }
    }
}

