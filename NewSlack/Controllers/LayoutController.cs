using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Newtonsoft.Json;
using Slack.Common.Attributes;
using Slack.Common.Encryption;
using Slack.Identity.Entities;
using Slack.Identity.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Slack.Controllers
{
    [Authorize]
    public class LayoutController : Controller
    {
        public ApplicationUserManager UserManager => HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
        public ApplicationRoleManager RoleManager => HttpContext.GetOwinContext().GetUserManager<ApplicationRoleManager>();

        [AjaxOnly]
        public async Task<ActionResult> EmailConfirmModalWindow()
        {
            var user = await UserManager.FindByNameAsync(User.Identity.Name);

            if (!(await UserManager.IsEmailConfirmedAsync(user.Id))) //if account is not confirmed
            {
                Encryptor encryptor = new Encryptor();

                ViewBag.link = Url.Action("SendEmailConfirmLink","Account", new { userId = encryptor.Encrypt(user.Id)});
                return PartialView("EmailConfirmModalWindow");
            }
            else return null;
        }

        [AjaxOnly]
        public async Task<string> GetCurrentUser()
        {
           return JsonConvert.SerializeObject(await UserManager.FindByIdAsync(User.Identity.GetUserId()));
        }
    }
}