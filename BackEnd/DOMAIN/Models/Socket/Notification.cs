using DOMAIN.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOMAIN.Models.Socket
{
    public class Notification
    {
        public dynamic Data { get; set; }
        public NotificationType type { get; set; }
        public string? receiverConnection { get; set; } = null;
    }
}
