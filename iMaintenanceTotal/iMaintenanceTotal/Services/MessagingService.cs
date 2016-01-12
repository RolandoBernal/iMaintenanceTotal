using iMaintenanceTotal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Twilio;

namespace iMaintenanceTotal.Services
{
    public class MessagingService
    {
        private static MaintTaskRepository repo = new MaintTaskRepository();
        public static void SendMessages(IEnumerable<MaintTask> tasks)
        {
            // Find your Account Sid and Auth Token at twilio.com/user/account
            string AccountSid = "ACa934fe8648ea1d66ea735d8124d80747";
            string AuthToken = "b9addbbf41f78e9fdcebc951ef764e5a";
 
            var twilio = new TwilioRestClient(AccountSid, AuthToken);
            foreach (var task in tasks)
            {
                var message = twilio.SendMessage("+16159885814", "+16152903050", "TEST", "");
            }
        }

        public static void SendReminders()
        {
            var tasks = repo.GetCurrentMaintTask();
            SendMessages(tasks);
        }

    }
}