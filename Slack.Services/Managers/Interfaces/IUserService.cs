using Slack.Identity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slack.Services.Interfaces
{
    public interface IUserService
    {
        Task<string> GenerateUserLogin(int length);

    }
}
