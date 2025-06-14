using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL
{
    public sealed class BALFactory
    {
        private BALFactory() { }

        private static BALFactory instance = null;
        public static BALFactory Instance
        {
            get 
            { 
                if (instance == null)
                {
                    instance = new BALFactory();
                }
                return instance;
            }
        }


        /// <summary>
        /// UsersBAL
        /// </summary>
        public UsersBAL usersBAL
        {
            get
            {
                return new UsersBAL();
            }
        }

        /// <summary>
        /// AdminUsersBAL
        /// </summary>
        public AdminUsersBAL AdminUsersBAL
        {
            get
            {
                return new AdminUsersBAL();
            }
        }

        /// <summary>
        /// FeedBackBAL
        /// </summary>
        public FeedBackBAL FeedBackBAL
        {
            get
            {
                return new FeedBackBAL();
            }
        }

        /// <summary>
        /// AdmissionBAL
        /// </summary>
        public AdmissionBAL AdmissionBAL
        {
            get
            {
                return new AdmissionBAL();
            }
        }

        /// <summary>
        /// StudentsBAL
        /// </summary>
        public StudentsBAL StudentsBAL
        {
            get
            {
                return new StudentsBAL();
            }
        }
    }
}
