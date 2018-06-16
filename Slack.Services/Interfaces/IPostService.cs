using Slack.Data.Entities;
using Slack.Identity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slack.Services.Interfaces
{
    public interface IPostService
    {
        Task<bool> AddAsync(string userId, object[] content);

        Tuple<IEnumerable<Post>, IEnumerable<ApplicationUser>> Get(int skip, int take, bool only, string userId);
    }
}
