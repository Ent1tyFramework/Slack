using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Slack.Identity.Contexts;
using Slack.Identity.Entities;
using Slack.Identity.Services;
using Slack.Identity.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slack.Identity.Managers
{
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(UserStore<ApplicationUser> userStore) : base(userStore) { }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options,
            IOwinContext context)
        {
            var manager = new ApplicationUserManager(
                new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>()));

            manager.UserValidator = new CustomUserValidator(manager);
            manager.EmailService = new EmailService();

            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }

        public List<ApplicationUser> Where(Func<ApplicationUser, bool> func)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
               return context.Users.Where(func).ToList();
            }
        }

        public async Task<List<ApplicationUser>> WhereAsync(Func<ApplicationUser, bool> func)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
               return await Task.Run(() => { return context.Users.Where(func).ToList(); });
            }
        }
    }
}
