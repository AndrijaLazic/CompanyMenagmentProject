using DOMAIN.Models.Socket;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class SharedDB
    {
        private ConcurrentDictionary<int, UserConnection> _onlineWorkers = new ConcurrentDictionary<int, UserConnection>();
        public void setWorkerOnline(int workerId, UserConnection connection)
        {
            _onlineWorkers.TryAdd(workerId, connection);
        }

        public void SetWorkerChatOnline(int workerId, string connection)
        {
            _onlineWorkers.TryGetValue(workerId, out UserConnection? conn);
            conn!.chatConnection = connection;
        }

        public void SetWorkerChatOffline(int workerId)
        {
            _onlineWorkers.TryGetValue(workerId, out UserConnection? conn);
            conn!.chatConnection = null;
        }

        public void SetWorkerOffline(int workerId)
        {
            _onlineWorkers.TryRemove(workerId, out UserConnection? conn);
        }

        public UserConnection? GetUserConn(int workerId)
        {
            _onlineWorkers.TryGetValue(workerId, out UserConnection? conn);
            return conn;
        }
    }
}
