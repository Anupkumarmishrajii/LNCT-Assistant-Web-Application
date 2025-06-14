using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL
{
    public sealed class AdmissionBAL
    {
        internal AdmissionBAL() { }


        /// <summary>
        /// InsertAdmission
        /// </summary>
        /// <param name="objAdmissionDTO"></param>
        /// <returns>InsertAdmission</returns>
        public async Task<AdmissionDTO> InsertAdmission(AdmissionDTO objAdmissionDTO)
        {
            return await DALFactory.Instance.AdmissionDAL.InsertAdmission(objAdmissionDTO);
        }

        /// <summary>
        /// GetAllAdmission
        /// </summary>
        /// <returns>GetAllAdmission</returns>
        public async Task<List<AdmissionDTO>> GetAllAdmission()
        {
            return await DALFactory.Instance.AdmissionDAL.GetAllAdmission();
        }

        /// <summary>
        /// GetAdmissionByID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>GetAdmissionByID</returns>
        public async Task<AdmissionDTO> GetAdmissionByID( long id)
        {
            return await DALFactory.Instance.AdmissionDAL.GetAdmissionByID(id);
        }


        /// <summary>
        /// UpdateAdmission
        /// </summary>
        /// <param name="objAdmissionDTO"></param>
        /// <returns>UpdateAdmission</returns>
        public async Task<AdmissionDTO> UpdateAdmission(AdmissionDTO objAdmissionDTO)
        {
            return await DALFactory.Instance.AdmissionDAL.UpdateAdmission(objAdmissionDTO);
        }

    }
}
