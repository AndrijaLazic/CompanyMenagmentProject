using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOMAIN.Models.Socket
{
    public class SocketEventAggregator
    {
        private event EventHandler<Notification> notificationSub;

        public void Subscribe(EventHandler<Notification> handler)
        {
            notificationSub += handler;
        }

        public void Publish(Notification notification)
        {
            notificationSub.Invoke(this, notification);
        }
    }
}
