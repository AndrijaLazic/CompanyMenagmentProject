using DOMAIN.Models.DTR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOMAIN.Models.Socket
{
    public interface INotificationsHub
    {
        public Task ReceiveNotification(Notification message);
        public Task UserOnline(string message);
        public Task UserOffline(string message);
        public Task ValidationError(string message);
    }
}
