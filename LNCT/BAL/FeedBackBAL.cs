using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL
{
    public sealed class FeedBackBAL
    {
        internal FeedBackBAL() { }

        /// <summary>
        /// InsertFeedBack
        /// </summary>
        /// <param name="objFeedBackDTO"></param>
        /// <returns>InsertFeedBack</returns>
        public async Task<FeedBackDTO> InsertFeedBack(FeedBackDTO objFeedBackDTO)
        {
            return await DALFactory.Instance.FeedBackDAL.InsertFeedBack(objFeedBackDTO);
        }

        /// <summary>
        /// GetAllFeedBack
        /// </summary>
        /// <returns>GetAllFeedBack</returns>
        public async Task<List<FeedBackDTO>> GetAllFeedBack()
        {
            return await DALFactory.Instance.FeedBackDAL.GetAllFeedBack();
        }

        /// <summary>
        /// GetFeedBackByID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>GetFeedBackByID</returns>
        public async Task<FeedBackDTO> GetFeedBackByID(long id)
        {
            return await DALFactory.Instance.FeedBackDAL.GetFeedBackByID(id);
        }

        /// <summary>
        /// UpdateFeedBack
        /// </summary>
        /// <param name="objFeedBackDTO"></param>
        /// <returns>UpdateFeedBack</returns>
        public async Task<FeedBackDTO> UpdateFeedBack(FeedBackDTO objFeedBackDTO)
        {
            return await DALFactory.Instance.FeedBackDAL.UpdateFeedBack(objFeedBackDTO);
        }
    }
}
