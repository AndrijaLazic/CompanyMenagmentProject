﻿using DOMAIN.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOMAIN.Abstractions
{
    public interface IUserDataDB
    {
        public int InsertNewUser(User user);
        public User GetUser(string mail);
        public User GetUser(int id);
        public void RemoveUser(int id);
        public User[] GetAllUsers();
        public WorkerType[] GetWorkerTypes();
    }
}
