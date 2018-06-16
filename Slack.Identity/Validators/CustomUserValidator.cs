using Microsoft.AspNet.Identity;
using Slack.Identity.Entities;
using Slack.Identity.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Slack.Identity.Validators
{
    public class CustomUserValidator : UserValidator<ApplicationUser>
    {
        public ApplicationUserManager UserManager;

        public CustomUserValidator(ApplicationUserManager userManager) : base(userManager)
        {
            this.UserManager = userManager;
        }

        public override async Task<IdentityResult> ValidateAsync(ApplicationUser user)
        {
            base.AllowOnlyAlphanumericUserNames = false;
            var result = await base.ValidateAsync(user);

            int count = result.Errors.Count();
            if (result.Errors.Count() > 0)
            {
                List<string> errors = result.Errors.ToList();

                string errorName = $"Name {user.UserName} is already taken.";
                bool isRemoved = errors.Remove(errorName);

                if (isRemoved)
                    errors.Add($"Email \"{user.UserName}\" is already registered.");

                result = new IdentityResult(errors);
            }

            if (result.Errors.Count() == 0)
                return IdentityResult.Success;
            else return result;
        }
    }
}
