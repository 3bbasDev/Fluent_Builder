using Fluent_Builder_With_Recursive_Generics.Firebase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fluent_Builder_With_Recursive_Generics.SendNoti
{
    public class Send
    {
        private Firebase.Firebase firebase = new Firebase.Firebase();
        public IFirebase _firebase => firebase ?? new Firebase.Firebase();

        public class Builder : Receiver<Builder>
        {

        }

        public static Builder New => new();

        public string Type { get; set; }
        public Guid? TypeId { get; set; }
        public string Title { get; set; }

        public (Guid, string) Sender { get; set; }

        public (List<Guid>, string) Receiver { get; set; }


    }
    public class SendBuild
    {
        protected Send Send = new();
        public Send Build() => Send;
    }
    public class SendInfoBuld<Self>
        : SendBuild
        where Self : SendInfoBuld<Self>
    {

        public Self TypeBuld(string type, Guid? typeId, string title)
        {
            Send.Type = type;
            Send.TypeId = typeId;
            Send.Title = title;
            return (Self)this;
        }
    }

    public class Sender<Self>
        : SendInfoBuld<Sender<Self>>
        where Self : Sender<Self>
    {
        public Self SenderInfo(Guid SenderId, string Body)
        {
            Send.Sender = (SenderId, Body);

            return (Self)this;
        }
    }

    public class Receiver<Self>
        : Sender<Receiver<Self>>
        where Self : Receiver<Self>
    {
        public Self ReceiverInfo(List<Guid> ReceiverId, string Body)
        {
            Send.Receiver = (ReceiverId, Body);
            return (Self)this;
        }
    }
}
