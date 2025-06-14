using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public sealed class DALFactory
    {
        public DALFactory() { }
        private static DALFactory instance = null;
        public static DALFactory Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DALFactory();
                }
                return instance;
            }
        }

        /// <summary>
        /// UsersDAL
        /// </summary>
      public  UsersDAL UsersDAL
      {
            get
            {
                return new UsersDAL();
            }
      }

        /// <summary>
        /// AdminUsersDAL
        /// </summary>
        public AdminUsersDAL AdminUsersDAL
        {
            get
            {
                return new AdminUsersDAL();
            }
        }

        /// <summary>
        /// AdmissionDAL
        /// </summary>
        public AdmissionDAL AdmissionDAL
        {
            get
            {
                return new AdmissionDAL();
            }
        }

        /// <summary>
        /// FeedBackDAL
        /// </summary>
        public FeedBackDAL FeedBackDAL
        {
            get
            {
                return new FeedBackDAL();
            }
        }

        /// <summary>
        /// StudentsDAL
        /// </summary>
        public StudentsDAL StudentsDAL
        {
            get
            {
                return new StudentsDAL();
            }
        }
    }
}
