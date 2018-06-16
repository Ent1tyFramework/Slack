using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
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

namespace Slack.Util
{
    public class NinjectRegistrations : NinjectModule
    {
        public override void Load()
        {
            var userStore = new UserStore<ApplicationUser>(new ApplicationDbContext());
            var userManager = new ApplicationUserManager(userStore);

            var dbContext = new DataDbContext();
            var repositoryManager = new RepositoryManager(dbContext);

            Bind<UserManager<ApplicationUser>>().To<ApplicationUserManager>().WithConstructorArgument("userStore", userStore);

            Bind<IRepositoryManager>().To<RepositoryManager>().WithConstructorArgument("dbContext", dbContext);
            Bind<ICacheRepository>().To<CacheRepository>();
            Bind<IServicesManager>().To<ServicesManager>().WithConstructorArgument("userManager", userManager)
                .WithConstructorArgument("repositoryManager", repositoryManager);


        }
    }
}