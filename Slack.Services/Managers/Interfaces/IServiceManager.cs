using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slack.Services.Interfaces
{
    public interface IServicesManager
    {
        IPostService PostService { get; }

        ISubService SubService { get; }

        IUserService UserService { get; }

        IDialogService DialogService { get; }

        IMessageService MessageService { get; }
    }
}
