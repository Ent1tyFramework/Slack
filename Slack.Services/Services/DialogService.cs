using Newtonsoft.Json;
using Slack.Data.Entities;
using Slack.Data.Interfaces;
using Slack.Identity.Managers;
using Slack.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slack.Services.Services
{
    public class DialogService : IDialogService
    {
        private readonly ApplicationUserManager userManager;
        private readonly IRepositoryManager repositoryManager;

        public DialogService(ApplicationUserManager userManager, IRepositoryManager repositoryManager)
        {
            this.userManager = userManager;
            this.repositoryManager = repositoryManager;
        }

        public async Task<string> GetInterlocutorAsync(Dialog dialog, string userId)
        {
            if (dialog == null || userId == null)
                return null;

            var user = await userManager.FindByIdAsync(userId);

            if (dialog.Private && user != null)
            {
                //get users of dialog
                var userIds = JsonConvert.DeserializeObject<List<string>>(dialog.Users);

                if (userIds != null)
                {
                    foreach (var uId in userIds)
                    {
                        //if user is interlocutor
                        if (user.Id == uId)
                            continue;

                        //find user
                        var findUser = await userManager.FindByIdAsync(uId);
                        if (user != null)
                            return JsonConvert.SerializeObject(new { Id = dialog.Id, User = findUser });
                    }
                }
            }
            return null;
        }

        public async Task<List<Dialog>> GetAsync(string userId,
            bool withoutHiddens = true)
        {
            if (userId == null)
                return null;

            var user = await userManager.FindByIdAsync(userId);

            if (user != null)
            {
                var dialogs = new List<Dialog>();

                //find user dialogs 
                var allDialogs = (await repositoryManager.Set<Dialog>()
                    .WhereAsync(d => d.Users.Contains(userId)
                    && d.Messages.Count > 0)).ToList();

                if (allDialogs != null && withoutHiddens)
                {
                    foreach (var dialog in allDialogs)
                    {
                        if (!String.IsNullOrEmpty(dialog.HiddenUsers))
                        {
                            //check is current user among hidden users
                            bool isHidden = dialog.HiddenUsers.Contains(userId);

                            if (isHidden)
                            {
                                continue; //skip this iteration
                            }
                        }
                        dialogs.Add(dialog); //add dialog to correct dialogs
                    }

                    return dialogs;
                }

                return allDialogs;
            }

            return null;
        }

        public async Task<Dialog> AddAsync(string currentId, string userId)
        {
            try
            {
                if (currentId == null || userId == null)
                    return null;

                var currentUser = await userManager.FindByIdAsync(currentId);
                var user = await userManager.FindByIdAsync(userId);

                if (currentUser != null && user != null)
                {
                    //create new dialog object
                    var dialog = new Dialog();

                    //find dialog in database, in case it's already created
                    dialog = await repositoryManager.Set<Dialog>()
                           .FirstAsync(d => d.Users.Contains(currentId)
                           && d.Users.Contains(user.Id) && d.Private == true);

                    //if dialog isn't created
                    if (dialog == null)
                    {
                        //create new dialog
                        dialog = new Dialog()
                        {
                            Users = JsonConvert.SerializeObject(new List<string> { currentId, user.Id }),
                            Private = true,
                            HiddenUsers = "[]" //improvise empty array
                        };

                        //add to database
                        bool isAdded = await repositoryManager.Set<Dialog>().AddAsync(dialog);

                        if (!isAdded)
                            return null;
                    }

                    return dialog;
                }
            }
            catch (Exception ex)
            { }

            return null;
        }

        public async Task<bool> HiddenAsync(int dialogId, string userId)
        {
            if (userId == null)
                return false;

            var user = await userManager.FindByIdAsync(userId);

            var dialog = await repositoryManager.Set<Dialog>().FirstAsync(d => d.Id == dialogId);

            if (dialog != null && user != null)
            {
                var hiddenUsers = new List<string>();

                if (!String.IsNullOrEmpty(dialog.HiddenUsers))
                {
                    //deserialize all hidden users
                    hiddenUsers = JsonConvert.DeserializeObject<List<string>>(dialog.HiddenUsers);

                    //if user already added
                    if (hiddenUsers.Contains(user.Id))
                        return false;
                }

                //add curent user
                hiddenUsers.Add(user.Id);

                //serialize hidden users
                dialog.HiddenUsers = JsonConvert.SerializeObject(hiddenUsers);

                return await repositoryManager.Set<Dialog>().UpdateAsync(dialog);
            }

            return false;
        }

        public async Task<bool> UnhiddenAsync(int dialogId, string userId)
        {
            if (userId == null)
                return false;

            var user = await userManager.FindByIdAsync(userId);

            var dialog = await repositoryManager.Set<Dialog>().FirstAsync(d => d.Id == dialogId);

            if (dialog != null && user != null)
            {
                var hiddenUsers = new List<string>();

                if (!String.IsNullOrEmpty(dialog.HiddenUsers))
                {
                    //deserialize all hidden users
                    hiddenUsers = JsonConvert.DeserializeObject<List<string>>(dialog.HiddenUsers);

                    if (hiddenUsers.Contains(userId))
                    {
                        //remove curent user
                        hiddenUsers.Remove(userId);

                        //serialize hidden users
                        dialog.HiddenUsers = JsonConvert.SerializeObject(hiddenUsers);
                    }

                    return await repositoryManager.Set<Dialog>().UpdateAsync(dialog);
                }
            }

            return false;
        }
    }
}
