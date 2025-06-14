using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL
{
    public sealed class StudentsBAL
    {
        internal StudentsBAL() { }


        /// <summary>
        /// GetPasswordByEmail
        /// </summary>
        /// <param name="email"></param>
        /// <returns>GetPasswordByEmail</returns>
        public async Task<StudentsDTO> GetPasswordByEmail(string email)
        {
            return await DALFactory.Instance.StudentsDAL.GetPasswordByEmail(email);
        }
    }
}
