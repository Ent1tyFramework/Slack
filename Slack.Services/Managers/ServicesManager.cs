using Slack.Data.Interfaces;
using Slack.Identity.Managers;
using Slack.Services.Interfaces;
using Slack.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slack.Services.Managers
{
    public class ServicesManager : IServicesManager
    {
        private readonly ApplicationUserManager userManager;
        private readonly IRepositoryManager repositoryManager;

        public ServicesManager(ApplicationUserManager userManager, IRepositoryManager repositoryManager)
        {
            this.userManager = userManager;
            this.repositoryManager = repositoryManager;
        }

        public IPostService PostService => new PostService(userManager, repositoryManager);

        public ISubService SubService => new SubService(userManager, repositoryManager);

        public IUserService UserService => new UserService(userManager);

        public IDialogService DialogService  => new DialogService(userManager,repositoryManager);

        public IMessageService MessageService => new MessageService(userManager, repositoryManager);
    }
}
