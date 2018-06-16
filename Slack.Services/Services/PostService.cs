using Microsoft.AspNet.Identity;
using Slack.Data.Entities;
using Slack.Data.Interfaces;
using Slack.Identity.Entities;
using Slack.Identity.Managers;
using Slack.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Slack.Services
{
    public class PostService : IPostService
    {
        private readonly ApplicationUserManager userManager;
        private readonly IRepositoryManager repositoryManager;

        private readonly bool isAuthenticated;
        private readonly string currentId;

        public PostService(ApplicationUserManager userManager, IRepositoryManager repositoryManager)
        {
            isAuthenticated = Thread.CurrentPrincipal.Identity.IsAuthenticated;
            currentId = Thread.CurrentPrincipal.Identity.GetUserId();

            this.userManager = userManager;
            this.repositoryManager = repositoryManager;
        }

        public async Task<bool> AddAsync(string userId, object[] content)
        {
            var user = await userManager.FindByIdAsync(userId);

            if (user != null)
            {
                for (int i = 0; i < content.Length; i++)
                {
                    Post post = new Post()
                    {
                        UserId = user.Id,
                        Content = content[i].ToString(),
                        PublishTime = DateTime.Now
                    };

                    //add to database
                    bool isAdded = await repositoryManager.Set<Post>().AddAsync(post);

                    if (!isAdded)
                        return false;
                }
                return true;
            }
            else return false;
        }

        public Tuple<IEnumerable<Post>, IEnumerable<ApplicationUser>> Get(int skip, int take, bool only, string userId)
        {
            if (userId == null)
                userId = currentId;

            var user = userManager.FindById(userId);

            if (user != null)
            {
                //posts
                var posts = new List<Post>();

                //user's posts
                posts.AddRange(repositoryManager.Set<Post>().Where(p => p.UserId == user.Id));

                if (!only)
                {
                    //subscriptions
                    var subs = repositoryManager.Set<Subscribe>()
                        .Where(s => s.UserFromId == user.Id).ToList();

                    if (subs.Count > 0)
                    {
                        //posts from user and him subs
                        foreach (var sub in subs)
                        {
                            posts.AddRange(repositoryManager.Set<Post>().Where(p => p.UserId == sub.UserToId));
                        }

                    }
                }

                //order by descending for skip and take fields
                posts = posts.OrderByDescending(p => p.Id).Skip(skip).Take(take).ToList();

                //get users
                var users = new List<ApplicationUser>();
                if (posts.Count > 0)
                {
                    foreach (var post in posts)
                    {
                        users.Add(userManager.Where(u => u.Id == post.UserId).FirstOrDefault());
                    }
                }

                return new Tuple<IEnumerable<Post>, IEnumerable<ApplicationUser>>(posts, users);
            }
            else return null;
        }
    }
}
