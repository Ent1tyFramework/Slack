using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Newtonsoft.Json;
using Slack.Data.Entities;
using Slack.Data.Interfaces;
using Slack.Identity.Entities;
using Slack.Services.Interfaces;

namespace Slack.Hubs
{
    [Authorize]
    public class MessageHub : Hub
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IServicesManager servicesManager;
        private readonly IRepositoryManager repositoryManager;

        public MessageHub(UserManager<ApplicationUser> userManager, IServicesManager servicesManager,
            IRepositoryManager repositoryManager)
        {
            this.userManager = userManager;
            this.servicesManager = servicesManager;
            this.repositoryManager = repositoryManager;
        }

        public override Task OnConnected()
        {
            string currentId = Context.User.Identity.GetUserId();

            var dialogs = repositoryManager.Set<Dialog>().Where(d => d.Users.Contains(currentId)
                                 && !d.HiddenUsers.Contains(currentId));

            if (dialogs != null)
            {
                dialogs.ToList().ForEach(d => Groups.Add(Context.ConnectionId, d.Id.ToString()));
            }

            return base.OnConnected();
        }

        public async Task Send(int dialogId, string content)
        {
            string currentId = Context.User.Identity.GetUserId();

            var currentUser = userManager.FindById(currentId);

            if (content != null)
            {
                //add message
                bool isAdded = await servicesManager.MessageService
                     .AddAsync(dialogId, currentUser.Id, content);

                if (isAdded)
                    Clients.Group(dialogId.ToString()).send(currentUser, content, dialogId);
                else
                    Clients.Group(dialogId.ToString()).error("Couldn't add message");
            }
        }
    }
}
