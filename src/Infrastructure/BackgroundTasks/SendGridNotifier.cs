using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace AspNetCoreIHostedService.Infrastructure.BackgroundTasks
{
    public class SendGridNotifier
    {
        public const string apiKey = "";
        
        public async Task Send(string to, string subject, string body)
        {
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("info@weatheromatic.com", "WeatherInfo");
            var toRecipient = new EmailAddress(to);
            
            var msg = MailHelper.CreateSingleEmail(from, toRecipient, subject, body, "");
            await client.SendEmailAsync(msg);
        }
    }
}
