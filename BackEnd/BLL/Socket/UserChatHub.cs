using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Services;
using DAL;
using DOMAIN.Abstractions;
using DOMAIN.Models.Database;
using DOMAIN.Models.Socket;
using DOMAIN.Shared;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;

namespace BLL.Socket
{
    public class UserChatHub: Hub<IUserChatHub>
    {
        private IUserDataDB _userDataDB;
        private readonly AppConfigClass _options;
        private SharedDB _sharedDB;

        public UserChatHub(IOptions<AppConfigClass> options, IUserDataDB userDataDB, SharedDB sharedDB)
        {
            _userDataDB = userDataDB;
            _options = options.Value;
            _sharedDB = sharedDB;
        }

        public override async Task OnConnectedAsync()
        {
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            _sharedDB.SetWorkerOffline(Context.Items["UserId"]?.ToString()!);
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

            _sharedDB.setWorkerOnline(userId, Context.ConnectionId);
            await Clients.All.UserOnline(userId);
        }
    }
}
