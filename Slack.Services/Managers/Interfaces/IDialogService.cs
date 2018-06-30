using Slack.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slack.Services.Interfaces
{
    public interface IDialogService
    {
        Task<string> GetInterlocutorAsync(Dialog dialog, string currentId);

        Task<List<Dialog>> GetAsync(string currentId, bool withoutHiddens = true);

        Task<Dialog> AddAsync(string currentId, string userId);

        Task<bool> HiddenAsync(int dialogId, string userId);

        Task<bool> UnhiddenAsync(int dialogId, string userId);
    }
}
