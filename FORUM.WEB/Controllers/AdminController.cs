using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FORUM.BLL.Interfaces;
using FORUM.WEB.Mapping;
using FORUM.WEB.Models;
using PagedList;

namespace FORUM.WEB.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {
        IServiceBag _serviceBag;

        public AdminController(IServiceBag serviceBag)
        {
            _serviceBag = serviceBag;
        }
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AllUsers(int? page)
        {
            var users = MapperBag.UserMapper.Map(_serviceBag.UserService.GetAll());
            var model = users.ToPagedList(page ?? 1, 10);
            return View(model);
        }

        [HttpGet]
        public ActionResult EditUser(string userName)
        {
            try
            {
                var model = MapperBag.UserMapper.Map(_serviceBag.UserService.Find(userName));
                return View(model);
            }
             catch
            {
                return new HttpNotFoundResult();

             }

        }

        [HttpPost]
        public ActionResult EditUser(UserModel user)
        {
            if (user == null)
                return new HttpNotFoundResult();
            if (ModelState.IsValid)
            {
                var operationDetails = _serviceBag.UserService.UpdateRoles(MapperBag.UserMapper.Map(user));
                if (operationDetails.Succeeded)
                {

                    return RedirectToAction("AllUsers", "Admin");
                }
                ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
            }
            return View(user);
        }

        protected override void Dispose(bool disposing)
        {
            _serviceBag.Dispose();
            base.Dispose(disposing);
        }
    }
}