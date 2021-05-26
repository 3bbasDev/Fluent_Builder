using FirebaseAdmin.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fluent_Builder_With_Recursive_Generics.Firebase
{
    public interface IFirebase
    {
        Task SendNotificationWithTopic(Message message);
    }
    public class Firebase: IFirebase
    {
        public Firebase()
        {
            //var defaultApp = FirebaseKeys.defaultApp;
        }
        private async Task NotificationWithTopic(Message message)
        {
            Console.WriteLine("Body=" + message.Notification.Body);
            //var messaging = FirebaseMessaging.DefaultInstance;
            //var result = await messaging.SendAsync(message);
        }

        public async Task SendNotificationWithTopic(Message message)
        {
            await NotificationWithTopic(message);
        }

    }
}
