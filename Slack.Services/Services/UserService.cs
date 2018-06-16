using System;
using Slack.Identity.Managers;
using Slack.Services.Interfaces;
using System.Threading.Tasks;

namespace Slack.Services.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationUserManager userManager;

        public UserService(ApplicationUserManager userManager)
        {
            this.userManager = userManager;
        }

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
