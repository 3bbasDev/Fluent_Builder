using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Fluent_Builder_With_Recursive_Generics.SendNoti;
using Google.Apis.Auth.OAuth2;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Fluent_Builder_With_Recursive_Generics
{
    class Program
    {
        static void Main(string[] args)
        {
            var watch = new System.Diagnostics.Stopwatch();

            watch.Start();

            Send send1 = new()
            {
                Receiver = (new List<Guid> { new Guid("C598BD23-64E9-466B-8379-89C743E29278"), new Guid("C598BD23-64E9-466B-8379-89C743E29278") }, "Hello receiver"),
                Sender = (new Guid("C598BD23-64E9-466B-8379-89C743E29278"), "Hello sender"),
                Title = "Abbas",
                TypeId = new Guid("C598BD23-64E9-466B-8379-89C743E29278"),
                Type = "type"
            };

            Sending Sending1 = new(send1);

            Sending1.FireBase();
            Sending1.SMS();

            watch.Stop();

            Console.WriteLine($"1 Execution Time: {watch.ElapsedMilliseconds} ms");


            watch.Start();

            Send Send2 = Send.New
                .TypeBuld(type: "type", typeId: new Guid("C598BD23-64E9-466B-8379-89C743E29278"), title: "Abbas")
                .SenderInfo(new Guid("C598BD23-64E9-466B-8379-89C743E29278"), "Hello sender")
                .ReceiverInfo(ReceiverId: new List<Guid> { new Guid("C598BD23-64E9-466B-8379-89C743E29278"), new Guid("C598BD23-64E9-466B-8379-89C743E29278") }, Body: "Hello receiver")
                .Build();



            Sending Sending2 = new(Send2);

            Sending2.FireBase();
            Sending2.SMS();

            watch.Stop();

            Console.WriteLine($"2 Execution Time: {watch.ElapsedMilliseconds} ms");


            Console.WriteLine("Hello World!");
        }
    }

    public class Sending
    {
        private Send send;
        public Sending(Send Send)
        {
            send = Send;
            
        }
        public void SMS() => Console.WriteLine($"SMS {send.Title}");
        public void FireBase()
        {
            Console.WriteLine($"FireBase {send.Title}");

            send._firebase.SendNotificationWithTopic(new FirebaseAdmin.Messaging.Message
            {
                Topic = send.Sender.Item1.ToString(),
                Notification = new FirebaseAdmin.Messaging.Notification
                {
                    Body = send.Sender.Item2,
                    Title = send.Title
                },
                Data = new Dictionary<string, string>()
                {
                    ["click_Action"] = "FLUTTER_NOTIFICATION_CLICK",
                    ["type"] = send.Type,
                    ["id"] = send.TypeId.ToString()
                }
            }).GetAwaiter();
            send.Receiver.Item1.ForEach(f =>

            send._firebase.SendNotificationWithTopic(new FirebaseAdmin.Messaging.Message
            {
                Topic = f.ToString(),
                Notification = new FirebaseAdmin.Messaging.Notification
                {
                    Body = send.Receiver.Item2,
                    Title = send.Title
                },
                Data = new Dictionary<string, string>()
                {
                    ["click_Action"] = "FLUTTER_NOTIFICATION_CLICK",
                    ["type"] = send.Type,
                    ["id"] = send.TypeId.ToString()
                }
            }).GetAwaiter()
            );
        }
        public void Db() => Console.WriteLine($"Db {send.Title}");

    }

    
   

}
