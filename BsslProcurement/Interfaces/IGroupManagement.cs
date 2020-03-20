﻿using DcProcurement.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BsslProcurement.Interfaces
{
   public interface IGroupManagement
    {

        UserGroup CreateGroup(string groupName);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="grpId"></param>
        /// <exception cref="KeyNotFoundException">Thrown when no groups are found</exception>
        Task DeleteGroup(long id);

        void AddUserToGroup(string userId);

        void RemoveUserFromGroup(string userId);
        Task<UserGroup> GetById(long id);
        Task<IList<UserGroup>> GetAll();
    }
}
