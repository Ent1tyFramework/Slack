using System;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Slack.Data.Interfaces;
using Slack.Identity.Managers;
using Slack.Services.Interfaces;
using Slack.Services.Services;

namespace Slack.Services.Managers
{
    public class ServicesManager : IServicesManager
    {
        private readonly IRepositoryManager repositoryManager;

        public ServicesManager(IRepositoryManager repositoryManager)
        {
            this.repositoryManager = repositoryManager;
        }

        public IPostService PostService => new PostService(repositoryManager);

        public ISubService SubService => new SubService(repositoryManager);

        public IUserService UserService => new UserService();

        public IDialogService DialogService  => new DialogService(repositoryManager);

        public IMessageService MessageService => new MessageService(repositoryManager);
    }
}
