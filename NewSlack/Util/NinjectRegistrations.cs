using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.SignalR;
using Ninject;
using Ninject.Modules;
using Slack.Common.Cache;
using Slack.Common.Interfaces;
using Slack.Data.Contexts;
using Slack.Data.Interfaces;
using Slack.Data.Managers;
using Slack.Identity.Contexts;
using Slack.Identity.Entities;
using Slack.Identity.Managers;
using Slack.Services.Interfaces;
using Slack.Services.Managers;
using System;
using System.Linq;
using System.Web;

namespace Slack.Util
{
    public class NinjectRegistrations : NinjectModule
    {
        public override void Load()
        {
            var dbContext = new DataDbContext();
            var repositoryManager = new RepositoryManager(dbContext);

            Bind<IRepositoryManager>().To<RepositoryManager>()
                .WithConstructorArgument("dbContext", dbContext);
            Bind<ICacheRepository>().To<CacheRepository>();
            Bind<IServicesManager>().To<ServicesManager>()
                .WithConstructorArgument("repositoryManager", repositoryManager);

        }
    }
}