using Slack.Data.Entities;
using Slack.Data.Interfaces;
using Slack.Identity.Managers;
using Slack.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slack.Services.Services
{
    public class MessageService : IMessageService
    {
        private readonly ApplicationUserManager userManager;
        private readonly IRepositoryManager repositoryManager;

        public MessageService(ApplicationUserManager userManager, IRepositoryManager repositoryManager)
        {
            this.userManager = userManager;
            this.repositoryManager = repositoryManager;
        }

        public async Task<bool> AddAsync(int dialogId, string userId, string content)
        {
            try
            {
                if (userId == null || content == null)
                    return false;

                var user = await userManager.FindByIdAsync(userId);

                var dialog = await repositoryManager.Set<Dialog>()
                     .FirstWithIncludesAsync(d => d.Id == dialogId, i => i.Messages);

                if (user == null || dialog == null)
                    return false;

                if (dialog.Users.Contains(userId))
                {
                    var message = new Message()
                    {
                        Dialog = dialog,
                        Content = content,
                        UserId = userId,
                        PublishTime = DateTime.Now
                    };

                    return await repositoryManager.Set<Message>().AddAsync(message);
                }
            }
            catch (Exception ex)
            { }

            return false;
        }
    }
}
