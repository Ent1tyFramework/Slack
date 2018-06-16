using System;
using System.Threading.Tasks;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin;
using Owin;
using Slack.Identity.Contexts;
using Slack.Identity.Managers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;
using Slack.Hubs;
using Slack.Data.Managers;
using Slack.Data;
using Slack.Identity.Entities;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.Google;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Slack.Services.Managers;
using Slack.Data.Contexts;

[assembly: OwinStartup(typeof(Slack.Startup))]

namespace Slack
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //настройка Identity
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationRoleManager>(ApplicationRoleManager.Create);

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/login")
            });

            //app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions() {
            //    ClientId = "71635730032-5clu1be6p7lh3vim6er56jkc3oip244h.apps.googleusercontent.com",
            //    ClientSecret = "fjOimgzKrDOGk4cte99QUu-T"
            //});

            //настройка SignalR
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var repositoryManager = new RepositoryManager(new DataDbContext());
            var serviceManager = new ServicesManager(userManager, repositoryManager);

            GlobalHost.DependencyResolver.Register(typeof(PostHub),
                () => new PostHub(userManager, serviceManager, repositoryManager));

            GlobalHost.DependencyResolver.Register(typeof(SubHub),
                () => new SubHub(userManager, serviceManager));

            GlobalHost.DependencyResolver.Register(typeof(MessageHub),
                () => new MessageHub(userManager, serviceManager, repositoryManager));

            app.MapSignalR();
        }
    }
}
