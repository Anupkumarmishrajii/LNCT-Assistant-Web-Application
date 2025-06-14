using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace BAL
{
    public sealed class UsersBAL
    {
        internal UsersBAL() { }

        public async Task<UsersDTO> InsertUser(UsersDTO objUsersDTO)
            => await DALFactory.Instance.UsersDAL.InsertUser(objUsersDTO);



        /// <summary>
        /// GetAllUsers
        /// </summary>
        /// <returns>GetAllUsers</returns>
        public async Task<List<UsersDTO>> GetAllUsers()
            => await DALFactory.Instance.UsersDAL.GetAllUsers();

        /// <summary>
        /// GetUserByID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>GetUserByID</returns>
        public async Task<UsersDTO> GetUserByID(long id)
        {
            return await DALFactory.Instance.UsersDAL.GetUserByID(id);
        }


        /// <summary>
        /// UpdateUser
        /// </summary>
        /// <param name="objUsersDTO"></param>
        /// <returns>UpdateUser</returns>
        public async Task<UsersDTO> UpdateUser(UsersDTO objUsersDTO)
           => await DALFactory.Instance.UsersDAL.UpdateUser(objUsersDTO);
    }
}
