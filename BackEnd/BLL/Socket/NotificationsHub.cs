using BLL.Services;
using DAL;
using DOMAIN.Abstractions;
using DOMAIN.Models.Socket;
using DOMAIN.Shared;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Socket
{
    public class NotificationsHub:Hub<INotificationsHub>
    {
        private IUserDataDB _userDataDB;
        private readonly AppConfigClass _options;
        private SharedDB _sharedDB;
        private CommunicationDB _communicationDB;

        public NotificationsHub(IOptions<AppConfigClass> options, IUserDataDB userDataDB, SharedDB sharedDB, CommunicationDB communicationDB)
        {
            _userDataDB = userDataDB;
            _options = options.Value;
            _sharedDB = sharedDB;
            _communicationDB = communicationDB;
        }

        public override async Task OnConnectedAsync()
        {
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            _sharedDB.SetWorkerOffline(int.Parse(Context.Items["UserId"]?.ToString()!));
            await Clients.All.UserOffline(Context.Items["UserId"]?.ToString()!);
        }

        public async Task JoinServer(string JWT)
        {

            string? userId = JWToken.ValidateToken(JWT, _options.JWTSettings);

            if (userId == null)
            {
                await Clients.Caller.ValidationError("InvalidJWT");
                return;
            }
            Context.Items["JWTtoken"] = JWT;
            Context.Items["UserId"] = userId;

            _sharedDB.setWorkerOnline(int.Parse(userId), new UserConnection()
            {
                notificationConnection = Context.ConnectionId
            });

            _sharedDB.GetUserConn(int.Parse(userId))!.eventAggregator.Subscribe(SendNotification!);

            await Clients.All.UserOnline(userId);
        }

        public async void SendNotification(object sender, Notification notification)
        {
            if (notification.receiverConnection == null)
            {

            }
            else
            {
                await Clients.Client(notification.receiverConnection).ReceiveNotification(notification);
            }
        }

        
    }
}
