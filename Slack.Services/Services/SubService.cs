using Slack.Data.Entities;
using Slack.Data.Interfaces;
using Slack.Identity.Managers;
using Slack.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slack.Services
{
    public class SubService : ISubService
    {
        private readonly ApplicationUserManager userManager;
        private readonly IRepositoryManager repositoryManager;

        public enum SubStatus
        {
            Added,
            Removed
        }

        public SubService(ApplicationUserManager userManager, IRepositoryManager repositoryManager)
        {
            this.userManager = userManager;
            this.repositoryManager = repositoryManager;
        }

        public async Task<SubStatus> AddAsync(string fromId, string toId)
        {
            var userFrom = await userManager.FindByIdAsync(fromId);
            var userTo = await userManager.FindByIdAsync(toId);

            if (userFrom != null && userTo != null)
            {
                //if user is already subscribed
                if (await IsSubscriberAsync(userFrom.Id, userTo.Id))
                {
                    //unsubscribe
                    var sub = await repositoryManager.Set<Subscribe>().FirstAsync(
                        s => s.UserFromId == userFrom.Id
                        && s.UserToId == userTo.Id);

                    bool isRemoved = await repositoryManager.Set<Subscribe>().RemoveAsync(sub);

                    if (isRemoved)
                        return SubStatus.Removed;
                }
                else
                {
                    //subscribe
                    bool isAdded = await repositoryManager.Set<Subscribe>().AddAsync(
                        new Subscribe()
                        {
                            UserFromId = userFrom.Id,
                            UserToId = userTo.Id
                        });

                    if (isAdded)
                        return SubStatus.Added;
                }
            }

            return SubStatus.Removed;
        }

        public bool IsSubscriber(string FromId, string ToId)
        {
            return (repositoryManager.Set<Subscribe>().First(
                              s => s.UserFromId == FromId
                              && s.UserToId == ToId)) != null;
        }

        public async Task<bool> IsSubscriberAsync(string FromId, string ToId)
        {
            return (await repositoryManager.Set<Subscribe>().FirstAsync(
                              s => s.UserFromId == FromId
                              && s.UserToId == ToId)) != null;
        }
    }
}
