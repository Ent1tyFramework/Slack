using System;
using Slack.Identity.Managers;
using Slack.Services.Interfaces;
using System.Threading.Tasks;
using System.Collections.Generic;
using Slack.Identity.Entities;
using Microsoft.AspNet.Identity;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Slack.Identity.Contexts;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Slack.Services.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationUserManager userManager =
            new ApplicationUserManager(new UserStore<ApplicationUser>(new ApplicationDbContext()));

        public async Task<string> GenerateUserLogin(int length)
        {
            Start:
            Random random = new Random();

            string login = null;

            for (int i = 0; i < length; i++)
                login += random.Next(0, 10).ToString();

            var user = await userManager.FindByNameAsync(login);

            if (user == null)
                return login;
            else goto Start;
        }

    }
}
