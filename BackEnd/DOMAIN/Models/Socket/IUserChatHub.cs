using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOMAIN.Models.Socket
{
    public interface IUserChatHub
    {
        public Task ReceiveMessage(string message);
        public Task UserOnline(string message);
        public Task UserOffline(string message);
        public Task DisconnectedFromAppMessage(string message);
        public Task ReceiveSpecificMessage(int receiverId, string message);
        public Task ValidationError(string message);
    }
}
