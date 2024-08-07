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
        private ConcurrentDictionary<string, string> _onlineWorkers = new ConcurrentDictionary<string, string>();

        public void setWorkerOnline(string workerId, string connection)
        {
            _onlineWorkers.TryAdd(workerId, connection);
        }

        public void SetWorkerOffline(string workerId)
        {
            _onlineWorkers.TryRemove(workerId, out _);
        }

    }
}
