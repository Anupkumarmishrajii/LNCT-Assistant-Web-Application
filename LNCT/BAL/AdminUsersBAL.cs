﻿using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL
{
    public sealed class AdminUsersBAL
    {
        internal AdminUsersBAL() { }
        // hii this is for push code testing..


        /// <summary>
        /// GetPasswordByEmail
        /// </summary>
        /// <param name="email"></param>
        /// <returns>GetPasswordByEmail</returns>
        public async Task<AdminUsersDTO> GetPasswordByEmail(string email)
        {
            return await DALFactory.Instance.AdminUsersDAL.GetPasswordByEmail(email);
        }
        public async Task<AdminUsersDTO> GetPasswordByEmail2(string email)
        {
            return await DALFactory.Instance.AdminUsersDAL.GetPasswordByEmail(email);
        }


        /// <summary>
        /// GetAllAdminUsers
        /// </summary>
        /// <returns>GetAllAdminUsers</returns>
        public async Task<List<AdminUsersDTO>> GetAllAdminUsers()
           => await DALFactory.Instance.AdminUsersDAL.GetAllAdminUsers();

    }
}
