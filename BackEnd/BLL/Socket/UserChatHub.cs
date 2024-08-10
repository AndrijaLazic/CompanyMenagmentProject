using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Services;
using DAL;
using DOMAIN.Abstractions;
using DOMAIN.Exceptions.SQL;
using DOMAIN.Models.Database;
using DOMAIN.Models.DTO;
using DOMAIN.Models.DTR;
using DOMAIN.Models.Socket;
using DOMAIN.Shared;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace BLL.Socket
{
    public class UserChatHub: Hub<IUserChatHub>
    {
        private IUserDataDB _userDataDB;
        private readonly AppConfigClass _options;
        private SharedDB _sharedDB;
        private CommunicationDB _communicationDB;

        public UserChatHub(IOptions<AppConfigClass> options, IUserDataDB userDataDB, SharedDB sharedDB, CommunicationDB communicationDB)
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

            _sharedDB.setWorkerOnline(int.Parse(userId), Context.ConnectionId);
            await Clients.All.UserOnline(userId);
        }
        public async Task SendMessage(ChatMessageDTO message)
        {
            if (Context.Items["UserId"] == null)
            {
                throw new UserNotFound("JWTNotValid");
            }
            int myId = int.Parse(Context.Items["UserId"]!.ToString()!);
            string? otherUserConn = _sharedDB.GetUserConn(message.receiverId);
            bool chatCreated = false;
            //if exists in sql database
            UserCommunication? userCommunication = _communicationDB.GetCommunication(myId, message.receiverId);
            if (userCommunication == null)
            {
                _communicationDB.AddNewCommunication(myId,message.receiverId);
                userCommunication = _communicationDB.GetCommunication(myId, message.receiverId);
                chatCreated = true;
            }
            _communicationDB.AddMessage(message, userCommunication!.Id, myId);
            _communicationDB.IncreaseMessagesUnread(userCommunication!.Id, message.receiverId);

            UserCommunicationDTR dtr = UserCommunicationDTR.ToDTR(userCommunication);

            if (chatCreated)
            {
                await Clients.Client(Context.ConnectionId).NewChatCreated(dtr!);
                if (otherUserConn != null)
                {
                    await Clients.Client(otherUserConn!).NewChatCreated(dtr!);
                }
                    
            }
            if (otherUserConn != null) {
                await Clients.Client(otherUserConn!).ReceiveMessage(message);
            }
            
            
        }
    }
}
