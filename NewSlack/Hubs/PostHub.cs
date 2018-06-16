using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;
using Slack.Data.Entities;
using Slack.Data.Interfaces;
using Slack.Identity.Entities;
using Slack.Services.Interfaces;

namespace Slack.Hubs
{
    [Authorize]
    public class PostHub : Hub
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IServicesManager servicesManager;
        private readonly IRepositoryManager repositoryManager;

        public PostHub(UserManager<ApplicationUser> userManager, IServicesManager servicesManager,
            IRepositoryManager repositoryManager)
        {
            this.userManager = userManager;
            this.servicesManager = servicesManager;
            this.repositoryManager = repositoryManager;
        }

        public override Task OnConnected()
        {
            string currentId = Context.User.Identity.GetUserId();

            var user = userManager.FindById(currentId);

            if (user != null)
            {
                /*Each user adds a post to a group with name equals his id.
                  So, for getting his posts, should adding current connectionId to his group.*/

                //add to a current user group
                Groups.Add(Context.ConnectionId, currentId);

                //add to a subs groups
                var subs = repositoryManager.Set<Subscribe>()
                     .Where(s => s.UserFromId == currentId);

                if (subs != null)
                    subs.ToList().ForEach(s => Groups.Add(Context.ConnectionId, s.UserToId));
            }

            return base.OnConnected();
        }

        public async Task Send(object[] content)
        {
            if (Context.User.Identity.IsAuthenticated)
            {
                //current user id
                string currentId = Context.User.Identity.GetUserId();

                //add to database
                bool isAdded = await servicesManager.PostService.AddAsync(currentId, content);

                //output to client
                if (isAdded)
                {
                    var currentUser = await userManager.FindByIdAsync(currentId);

                    if (currentUser != null)
                        Clients.Group(currentId).send(content, currentUser);
                }
                else
                {
                    //if file is not added, return user error
                    Clients.Caller.error("Couldn't upload file!");
                }
            }
        }
    }
}