using Slack.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slack.Services.Interfaces
{
    public interface IMessageService
    {
        Task<bool> AddAsync(int dialogId, string userId, string content);
    }
}
