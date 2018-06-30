using Slack.Identity.Managers;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Threading.Tasks;
using System;
using Slack.Models.ViewModels;
using Slack.Identity.Entities;
using System.Linq;
using Microsoft.AspNet.Identity;
using Slack.Services.Interfaces;
using Slack.Common.Enums;

namespace Slack.Controllers
{
    public class AccountController : Controller
    {
        private ApplicationUserManager UserManager => HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
        private IAuthenticationManager AuthManager => HttpContext.GetOwinContext().Authentication;

        private readonly IServicesManager servicesManager;

        public AccountController(IServicesManager servicesManager)
        {
            this.servicesManager = servicesManager;
        }

        //Регистрация
        [Route("register")]
        public ActionResult Register()
        {
            return View("Register");
        }

        //Регистрирует пользователя и перенаправляет на /Account/SendEmailConfirmLink
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("register")]
        public async Task<ActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByEmailAsync(model.Email);

                if (user == null)
                {
                    user = new ApplicationUser
                    {
                        Email = model.Email,
                        UserName = await servicesManager.UserService.GenerateUserLogin(10),
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Country = model.Country,
                        City = model.City,
                        Birthday = model.Birthday
                    };

                    if (model.Gender == Gender.Male)
                        user.ImagePath = "/Content/Images/male_user_default.png";
                    else user.ImagePath = "/Content/Images/female_user_default.png";

                    var result = await UserManager.CreateAsync(user, model.Password);

                    if (result.Succeeded)
                        return RedirectToAction("SendEmailConfirmLink", new { userId = user.Id });
                    else result.Errors.ToList().ForEach(error => ModelState.AddModelError("", error));
                }
                else ModelState.AddModelError("Email", "Email already registered");
            }
            return View("Register");
        }

        //Отправляет ссылку для подтверждения емайла
        //GET: /Account/SendEmailConfirmLink
        public async Task<ActionResult> SendEmailConfirmLink(string userId, string returnUrl)
        {
            if (userId == null)
                return View("Error");

            var user = await UserManager.FindByIdAsync(userId);
            if (user != null && !(await UserManager.IsEmailConfirmedAsync(user.Id)))
            {
                string token = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                string callBack = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, token, returnUrl }, Request.Url.Scheme);

                await UserManager.SendEmailAsync(user.Id, "Подтверждение аккаунта", $"<h3>Для завершения регистрации," +
                   $" пожалуйста, перейдите по <a href=\"{callBack}\">этой ссылке</a>.</h3> ");

                string message = $"Registration message sent to {user.Email}";
                return View("SendConfirmationLink", model: message);
            }
            else return View("Error");
        }

        //Отправляет ссылку для создания нового пароля(т.е. когда пользователь забыл пароль)
        //GET: /Account/SendEmailPasswordResetLink
        public async Task<ActionResult> SendEmailPasswordResetLink(string userId, string returnUrl)
        {
            if (userId == null)
                return View("Error");

            var user = await UserManager.FindByIdAsync(userId);
            if (user != null && await UserManager.IsEmailConfirmedAsync(user.Id))
            {
                string token = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                string callBack = Url.Action("ResetPassword", "Account", new { userId = user.Id, token, returnUrl }, Request.Url.Scheme);

                await UserManager.SendEmailAsync(user.Id, "Восстановление пароля", $"<h3>Для создания нового " +
                     $"пароля, пожалуйста, перейдите по <a href=\"{callBack}\">этой ссылке</a>.</h3>");

                string message = $"Password recovery message sent to {user.Email}";
                return View("SendConfirmationLink", model: message);
            }
            else return View("Error");

        }

        /*Проверка кода подтверждения, отправленного на email.
          Также устанавливает значение True в строке EmailConfirm в базе данных для этого пользователя,
          если метод выполнится корректно.*/
        //GET: /Account/ConfirmEmail/
        public async Task<object> ConfirmEmail(string userId, string token, string returnUrl)
        {
            if (userId != null && token != null)
            {
                var result = await UserManager.ConfirmEmailAsync(userId, token);

                if (String.IsNullOrEmpty(returnUrl))
                {
                    if (result.Succeeded)
                    {
                        string message = "Thanks for confirming email!";
                        return View("ConfirmationSuccess", model: message);
                    }
                    else return View("Error");
                }
                else return Redirect(returnUrl);
            }
            else return View("Error");
        }

        //Вход
        [Route("login")]
        public ActionResult Login(string returnUrl)
        {
            ViewData["returnUrl"] = returnUrl;
            return View("Login");
        }

        //Авторизует пользователя и перенаправляет на url: returnUrl, если он указан
        //Если аккаунт не подтвержден, то перенаправляет на /Account/SendEmailConfirmLink
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("login")]
        public async Task<ActionResult> Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await UserManager.FindAsync(
                          (await UserManager.FindByEmailAsync(model.Email)).UserName, model.Password);

                    if (user != null)
                    {
                        var claims = await UserManager.CreateIdentityAsync(user,
                            DefaultAuthenticationTypes.ApplicationCookie);

                        AuthManager.SignOut();
                        AuthManager.SignIn(new AuthenticationProperties() { IsPersistent = true }, claims);

                        if (!String.IsNullOrEmpty(returnUrl))
                            return Redirect(returnUrl);
                        else return Redirect("/home");
                    }
                    else ModelState.AddModelError("", "Invalid login or password!");
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError("", "Invalid login or password!");
                }
            }
            ViewData["returnUrl"] = returnUrl;
            return View("Login");
        }

        //Выход
        [Route("signout")]
        public ActionResult SignOut()
        {
            AuthManager.SignOut();
            return Redirect("/home");
        }

        /// <summary>
        /// Восстановление пароля
        /// </summary>

        /*Запрашивает email пользователя для отправки кода подтверждения*/
        [Route("forgot-password")]
        public ActionResult ForgotPassword()
        {
            return View("ForgotPassword");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("forgot-password")]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByEmailAsync(model.Email);

                if (user != null && (await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    return RedirectToAction("SendEmailPasswordResetLink", new { userId = user.Id });
                }
                else ModelState.AddModelError("", $"Invalid email");
            }
            return View("ForgotPassword", model);
        }

        /*Запрашивает новый пароль у пользователя*/
        //GET: /Account/ResetPassword
        public async Task<ActionResult> ResetPassword(string userId, string token)
        {
            if (userId != null && token != null)
            {
                var user = await UserManager.FindByIdAsync(userId);

                if (user != null)
                {
                    ViewBag.user = user;
                    ViewBag.token = token;

                    return View("ResetPassword");
                }
            }
            return new HttpNotFoundResult();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordModel model)
        {
            var user = await UserManager.FindByIdAsync(model.UserId);
            if (user == null)
                return new HttpNotFoundResult();

            if (ModelState.IsValid)
            {
                var identityResult = await UserManager.ResetPasswordAsync(model.UserId, model.Token, model.Password);

                if (identityResult.Succeeded)
                {
                    string message = "Your password was changed.";
                    return View("ConfirmationSuccess", model: message);
                }
                else identityResult.Errors.ToList().ForEach(e => ModelState.AddModelError("", e));
            }

            ViewBag.user = user;

            return View("ResetPassword", model);
        }
    }
}