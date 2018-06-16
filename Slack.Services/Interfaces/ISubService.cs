using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Slack.Services.SubService;

namespace Slack.Services.Interfaces
{
    public interface ISubService
    {
        Task<SubStatus> AddAsync(string fromId, string toId);

        bool IsSubscriber(string FromId, string ToId);
        Task<bool> IsSubscriberAsync(string FromId, string ToId);
    }
}
