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
        private ConcurrentDictionary<int, string> _onlineWorkers = new ConcurrentDictionary<int, string>();
        public void setWorkerOnline(int workerId, string connection)
        {
            _onlineWorkers.TryAdd(workerId, connection);
        }

        public void SetWorkerOffline(int workerId)
        {
            _onlineWorkers.TryRemove(workerId, out _);
        }

        public string? GetUserConn(int workerId)
        {
            _onlineWorkers.TryGetValue(workerId, out string? conn);
            return conn;
        }
    }
}
