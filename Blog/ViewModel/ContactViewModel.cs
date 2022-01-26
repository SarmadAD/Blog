using Blog.Classes.ObjClasses;
using Blog.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.ViewModel
{
    public class ContactViewModel : BaseViewModel
    {

        private readonly MailSettings _mailSettings;
        private NavigationManager navigationManager;

        public User ContractUser { get; set; } = new User();
        public string Message { get; set; }

        public ContactViewModel(IOptions<MailSettings> mailSettings, NavigationManager navigationManager)
        {
            this._mailSettings = mailSettings.Value;
            this.navigationManager = navigationManager;
        }

        public async Task SendEmailAsync()
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(ContractUser.Email);
            email.To.Add(MailboxAddress.Parse(_mailSettings.Mail));
            //email.Subject = mailRequest.Subject;
            var builder = new BodyBuilder();

            //if (mailRequest.Attachments != null)
            //{
            //    byte[] fileBytes;
            //    foreach (var file in mailRequest.Attachments)
            //    {
            //        if (file.Length > 0)
            //        {
            //            using (var ms = new MemoryStream())
            //            {
            //                file.CopyTo(ms);
            //                fileBytes = ms.ToArray();
            //            }
            //            builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
            //        }
            //    }
            //}D:\dev\Sourcetree\Blog\Blog

            string FilePath = Directory.GetCurrentDirectory() + @"\wwwroot\welcome.html";
            StreamReader str = new StreamReader(FilePath);
            string MailText = str.ReadToEnd(); 
            str.Close();
            email.Subject = $"Nachricht von {ContractUser.Firstname}";
            MailText = MailText
                .Replace("[Vorname]", ContractUser.Firstname)
                .Replace("[Nachname]", ContractUser.Lastname)
                .Replace("[EMail]", ContractUser.Email)
                .Replace("[Message]", Message);
            builder.HtmlBody = MailText;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
            navigationManager.NavigateTo("/");
        }
    }
}
