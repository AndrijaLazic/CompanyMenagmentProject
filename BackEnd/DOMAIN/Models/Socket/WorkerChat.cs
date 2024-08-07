using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOMAIN.Models.Socket
{
    public class WorkerChat
    {
        public readonly string chatKey;
        public WorkerInfoSocket worker1;
        public WorkerInfoSocket worker2;
        public int ChatId;

        public WorkerChat(int user1Id, int user2Id, int chatId)
        {
            worker1 = new WorkerInfoSocket();
            worker1.Id = user1Id;
            worker2 = new WorkerInfoSocket();
            worker2.Id = user2Id;
            chatKey = $"{user1Id}" + "/" + $"{user2Id}";
            ChatId = chatId;
        }
    }

    public record WorkerInfoSocket
    {
        public int Id;
        public string userConnectionId;
    }
}
