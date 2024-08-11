using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOMAIN.Models.Socket
{
    public class UserConnection
    {
        public string? chatConnection { get; set; }
        public string? notificationConnection { get; set; }
        public SocketEventAggregator eventAggregator { get; set; } = new SocketEventAggregator();

    }
}
