using Slack.Data.Entities;
using Slack.Data.Interfaces;
using Slack.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using Slack.Data.Managers;
using Slack.Hubs;
using Slack.Identity.Managers;
using Microsoft.AspNet.Identity.Owin;
using Slack.Common.Interfaces;
using Slack.Identity.Entities;
using Newtonsoft.Json;
using Slack.Common.Attributes;
using System.IO;
using System.Threading;
using Slack.Models.ViewModels;
using Slack.Services.Interfaces;
using Newtonsoft.Json.Linq;
using Slack.Models.Entities;

namespace Slack.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private ApplicationUserManager UserManager => HttpContext.GetOwinContext()
            .GetUserManager<ApplicationUserManager>();

        private readonly IRepositoryManager repositoryManager;
        private readonly ICacheRepository cacheRepository;
        private readonly IServicesManager servicesManager;

        public HomeController(IRepositoryManager repositoryManager, ICacheRepository cacheRepository,
             IServicesManager servicesManager)
        {
            this.repositoryManager = repositoryManager;
            this.cacheRepository = cacheRepository;
            this.servicesManager = servicesManager;
        }

        public ActionResult News()
        {
            return View("News");
        }

        //i'm using synchronous methods to work with DB,
        //because child pages in asp.net mvc 5 can't be asynchronous
        [AllowAnonymous]
        public PartialViewResult Posts(int skip, int take, bool only, string userId,
            bool fromCache = false)
        {
            if (userId == null)
                userId = User.Identity.GetUserId();

            if (fromCache)
            {
                //get posts from cache, if any
                try
                {
                    var postsFromCache = (IEnumerable<Post>)cacheRepository.First("posts");
                    var usersFromCache = (IEnumerable<ApplicationUser>)cacheRepository.First("users");

                    if (postsFromCache != null && usersFromCache != null)
                    {
                        return PartialView("Posts", Tuple.Create(postsFromCache, usersFromCache));
                    }
                    else { }
                }
                catch { }
            }

            //get posts from database
            var tuple = servicesManager.PostService.Get(skip, take, only, userId);

            IEnumerable<Post> posts = tuple.Item1;
            IEnumerable<ApplicationUser> users = tuple.Item2;

            //add to cache
            cacheRepository.Add(posts, "posts");
            cacheRepository.Add(users, "users");

            return PartialView("Posts", tuple);
        }

        [AjaxOnly]
        [HttpPost]
        public JsonResult UploadImage()
        {
            var files = Request.Files;
            if (files.Count > 0)
            {
                string[] paths = new string[files.Count + 1];
                for (int i = 0; i < files.Count; i++)
                {
                    var file = files[i];

                    //if file is not image type(jpg,png)
                    if (!file.ContentType.Contains("jpg") && !file.ContentType.Contains("jpeg")
                        && !file.ContentType.Contains("png"))
                        throw new Exception("Couldn't upload file");

                    string virtualPath = $"/Content/Users/Images/{User.Identity.GetUserId()}";
                    string physicPath = Server.MapPath(virtualPath);

                    if (!Directory.Exists(physicPath))
                        Directory.CreateDirectory(physicPath);

                    physicPath += $"/{file.FileName}";//to save file
                    file.SaveAs(physicPath);

                    virtualPath += $"/{file.FileName}"; //in order to return full 
                    paths[i] = virtualPath; //virtual path to the client
                }
                return Json(JsonConvert.SerializeObject(paths));
            }
            else throw new Exception("Couldn't upload file");
        }

        [AllowAnonymous]
        [Route("user/{login}")]
        public async Task<ActionResult> UserProfile(string login)
        {
            if (login == null && User.Identity.IsAuthenticated)
                login = (await UserManager.FindByIdAsync(User.Identity.GetUserId())).UserName;

            var user = await UserManager.FindByNameAsync(login);

            if (user != null)
            {
                if (User.Identity.IsAuthenticated && User.Identity.GetUserId() == user.Id)
                {
                    return View("MyProfile", user);
                }
                else
                {
                    bool isSubscriber = false;

                    if (User.Identity.IsAuthenticated)
                        isSubscriber = await servicesManager.SubService.IsSubscriberAsync(User.Identity.GetUserId(), user.Id);

                    return View("AnotherProfile", Tuple.Create(user, isSubscriber));
                }
            }
            else return new HttpNotFoundResult();
        }

        [Route("settings")]
        public async Task<ActionResult> Settings()
        {
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                var editModel = new EditModel()
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Country = user.Country,
                    City = user.City,
                    Birthday = user.Birthday,
                    Image = user.ImagePath
                };

                return View("Settings", editModel);
            }
            else return View("Error");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("settings")]
        public async Task<ActionResult> Settings(EditModel model)
        {
            if (ModelState.IsValid)
            {
                string currentId = User.Identity.GetUserId();
                var user = await UserManager.FindByIdAsync(currentId);

                if (user != null)
                {
                    #region Change user values
                    user.FirstName = model.FirstName;
                    user.LastName = model.LastName;
                    user.Country = model.Country;
                    user.City = model.City;
                    user.Birthday = model.Birthday;
                    user.ImagePath = model.Image;
                    #endregion

                    var result = await UserManager.UpdateAsync(user);

                    if (!result.Succeeded)
                        result.Errors.ToList().ForEach(error => ModelState.AddModelError("", error));
                }
                else return View("Error");
            }
            return View("Settings", model);
        }

        [AllowAnonymous]
        [Route("search")]
        public ActionResult Search()
        {
            return View("Search");
        }

        [AjaxOnly]
        [HttpPost]
        [AllowAnonymous]
        [Route("search")]
        public PartialViewResult Search(string query)
        {
            if (query != null)
            {
                string[] words = query.Split();
                var users = new List<ApplicationUser>();

                foreach (string word in words)
                {
                    users.AddRange(UserManager.Where(u => u.FirstName.ToLower() == word.ToLower()
                    || u.LastName.ToLower() == word.ToLower()));
                }

                var isSubs = new Dictionary<string, bool>();

                if (User.Identity.IsAuthenticated)
                {
                    foreach (var user in users)
                    {
                        isSubs.Add(user.Id, servicesManager.SubService.IsSubscriber(User.Identity.GetUserId(), user.Id));
                    }
                }

                ViewBag.currentId = User.Identity.GetUserId();
                return PartialView("Users", Tuple.Create(users, isSubs));
            }
            return null;
        }

        [Route("dialogs")]
        public async Task<ActionResult> Dialogs()
        {
            string currentId = User.Identity.GetUserId();

            var dialogs = new List<Dialog>();
            var interlocutors = new List<UserFor>();

            //get all dialogs without hidden 
            var allDialogs = await servicesManager.DialogService.GetAsync(currentId);

            if (dialogs != null)
            {
                foreach (var dialog in allDialogs)
                {
                    if (dialog.Private)
                    {
                        //get interlocutor of current user in dialog
                        string interlocutorJson = await servicesManager.DialogService
                             .GetInterlocutorAsync(dialog, currentId);

                        if (interlocutorJson != null)
                        {
                            var interlocutor = JsonConvert.DeserializeObject<UserFor>(interlocutorJson);

                            interlocutors.Add(interlocutor);
                        }
                    }
                    dialogs.Add(dialog); //add dialog to correct dialogs
                }
            }

            //return dialogs
            return View("Dialogs", Tuple.Create(dialogs, interlocutors));
        }

        [Route("dialog/{dialogId}")]
        public async Task<ActionResult> Dialog(int dialogId)
        {
            string currentId = User.Identity.GetUserId();

            var dialog = await repositoryManager.Set<Dialog>()
                .FirstWithIncludesAsync(d => d.Id == dialogId,i => i.Messages);

            if (dialog == null)
                return View("Error");

            //unhide dialog, if he'is hidden
            bool isHidden = dialog.HiddenUsers.Contains(currentId);

            if (isHidden)
                await servicesManager.DialogService.UnhiddenAsync(dialog.Id, currentId);

            //get users
            var users = new List<UserFor>();

            foreach (var message in dialog.Messages)
            {
                var user = await UserManager.FindByIdAsync(message.UserId);

                if (user != null)
                    users.Add(new UserFor() { Id = message.Id, User = user });
            }

            if (dialog.Private)
            {
                //get interlocutor
                string interlocutorJson = await servicesManager.DialogService
                     .GetInterlocutorAsync(dialog, currentId);

                if (interlocutorJson == null)
                    return View("Error");

                var interlocutor = JsonConvert.DeserializeObject<UserFor>(interlocutorJson);

                return View("PrivateDialog", Tuple.Create(dialog.Messages.ToList(), users, interlocutor));
            }

            return View("Error");
        }

        [Route("dialog/create-private/{userId}")]
        public async Task<ActionResult> CreatePrivateDialog(string userId)
        {
            if (userId != null)
            {
                string currentId = User.Identity.GetUserId();

                var user = await UserManager.FindByIdAsync(userId);

                if (user != null && currentId != userId)
                {
                    //create new dialog
                    Dialog dialog = await servicesManager.DialogService
                       .AddAsync(currentId, user.Id);

                    if (dialog != null)
                    {
                        //redirect user to dialog
                        return Redirect($"/dialog/{dialog.Id}");
                    }
                }
            }
            return View("Error");
        }

        [AjaxOnly]
        [Route("dialog/hide")]
        public async Task<bool> HideDialog(int dialogId)
        {
            string currentId = User.Identity.GetUserId();

            var dialog = await repositoryManager.Set<Dialog>().FirstAsync(d => d.Id == dialogId);

            if (dialog != null)
                return await servicesManager.DialogService.HiddenAsync(dialog.Id, currentId);

            return false;
        }

        [AjaxOnly]
        [Route("dialog/unhide")]
        public async Task<bool> UnhideDialog(int dialogId)
        {
            string currentId = User.Identity.GetUserId();

            var dialog = await repositoryManager.Set<Dialog>().FirstAsync(d => d.Id == dialogId);

            if (dialog != null)
                return await servicesManager.DialogService.UnhiddenAsync(dialog.Id, currentId);

            return false;
        }
    }
}