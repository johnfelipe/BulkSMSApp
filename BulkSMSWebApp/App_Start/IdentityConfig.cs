using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using BulkSMSWebApp.Models;
using System.Net.Mail;
using System.Net.Mime;

namespace BulkSMSWebApp
{
    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
           sendMailAsync(message);
            // Plug in your email service here to send an email.
            return Task.FromResult(0);
        }

        private void sendMailAsync(IdentityMessage message)
          {
              try
              {
                  #region formatter
                  string text = string.Format("Por favor haga click en este enlace para {0}: {1}", message.Subject, message.Body);
                  string html = "Recientemente solicitaste reestablecer tu Contraseña de acceso al Sistema en nuestra plataforma SMS. \n puedes reestablecer tu Contraseña haciendo click en este: <a href=\"" + message.Body + "\"> enlace </a><br/>";
                  html += HttpUtility.HtmlEncode(@"o copia y pega el siguiente enlace en el navegador: " + message.Body);
                  #endregion

                  MailMessage msg = new MailMessage();
                  msg.From = new MailAddress("adminapplication@gmail.com");
                  msg.To.Add(new MailAddress(message.Destination));
                  msg.Subject = message.Subject;
                  msg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(text, null, MediaTypeNames.Text.Plain));
                  msg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(html, null, MediaTypeNames.Text.Html));

                  SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", Convert.ToInt32(587));
                  System.Net.NetworkCredential credentials = new System.Net.NetworkCredential("noguer1992@gmail.com", "87725568_Noguera?");
                  smtpClient.Credentials = credentials;
                  smtpClient.EnableSsl = true;
                  smtpClient.Send(msg);
              }
              catch (Exception ex)
              {
                  throw;
              }
            
        }
    }

    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your SMS service here to send a text message.
            return Task.FromResult(0);
        }
    }

    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context) 
        {
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>()));
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };

            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = false;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.
            manager.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<ApplicationUser>
            {
                MessageFormat = "Tu código de seguridad es {0}"
            });
            manager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<ApplicationUser>
            {
                Subject = "Código de Seguridad",
                BodyFormat = "Tu código de seguridad es {0}"
            });
            manager.EmailService = new EmailService();
            manager.SmsService = new SmsService();
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<ApplicationUser>
                        (dataProtectionProvider.Create("ASP.NET Identity")) 
                        {
                            TokenLifespan = TimeSpan.FromHours(2)
                        };
            }
            return manager;
        }
    }

    // Configure the application sign-in manager which is used in this application.
    public class ApplicationSignInManager : SignInManager<ApplicationUser, string>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user)
        {
            return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
        }

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }
    }
}
