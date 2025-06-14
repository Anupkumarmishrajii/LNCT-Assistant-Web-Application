using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace DAL
{
    public sealed class AdminUsersDAL
    {
        internal AdminUsersDAL() { }

        public async Task<AdminUsersDTO> GetPasswordByEmail(string email)
        {
            try
            {
                AdminUsersDTO objAdminUsersDTO = null;

                using (SqlConnection connection = new SqlConnection("Data Source=MISHRAJI\\SQLEXPRESS;Initial Catalog=WebLNCTAssistant;Integrated Security=True;Encrypt=True;Trust Server Certificate=True"))
                {
                    await connection.OpenAsync();
                    string sqlQuery = $@"Select
                                        AdminUsers.AdminUserID
                                        ,AdminUsers.Name
                                        ,AdminUsers.Email
                                        ,AdminUsers.Password
                                        ,AdminUsers.Status
                                        FROM AdminUsers WITH(NOLOCK)
                                        WHERE Status = 1 AND
                                        Email = @Email;
                                        ";
                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        command.Parameters.AddWithValue("Email", email);
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (reader.Read())
                            {
                                objAdminUsersDTO = new AdminUsersDTO();
                                objAdminUsersDTO.AdminUserID = reader.GetInt64(reader.GetOrdinal("AdminUserID"));
                                objAdminUsersDTO.Name = reader.GetString(reader.GetOrdinal("Name"));
                                objAdminUsersDTO.Email = reader.GetString(reader.GetOrdinal("Email"));
                                objAdminUsersDTO.Password = reader.GetString(reader.GetOrdinal("Password"));
                                objAdminUsersDTO.Status = reader.GetInt32(reader.GetOrdinal("Status"));
                            }
                        }
                    }
                    await connection.CloseAsync();
                    await connection.DisposeAsync();
                }

                return objAdminUsersDTO;
            }
            catch
            {
                throw;
            }
        }

      

        public async Task<List<AdminUsersDTO>> GetAllAdminUsers()
        {
            try
            {
                AdminUsersDTO? objAdminUsersDTO = null;
                List<AdminUsersDTO> lstAdminUsersDTO = new List<AdminUsersDTO>();
                using (SqlConnection connection = new SqlConnection("Data Source=MISHRAJI\\SQLEXPRESS;Initial Catalog=WebLNCTAssistant;Integrated Security=True;Encrypt=True;Trust Server Certificate=True"))
                {
                    await connection.OpenAsync();
                    String sqlQuery = $@"Select
                                         AdminUsers.AdminUserID
                                        ,AdminUsers.Name
                                        ,AdminUsers.Email
                                        ,AdminUsers.Status
                                        FROM AdminUsers WITH(NOLOCK);
                                            ";

                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                objAdminUsersDTO = new AdminUsersDTO();
                                objAdminUsersDTO.AdminUserID = reader.GetInt64(reader.GetOrdinal("AdminUserID"));
                                objAdminUsersDTO.Name = reader.GetString(reader.GetOrdinal("Name"));
                                objAdminUsersDTO.Email = reader.GetString(reader.GetOrdinal("Email"));
                                objAdminUsersDTO.Status = reader.GetInt32(reader.GetOrdinal("Status"));
                                lstAdminUsersDTO.Add(objAdminUsersDTO);
                            }
                        }
                    }

                    await connection.CloseAsync();
                    await connection.DisposeAsync();
                }

                return lstAdminUsersDTO;
            }
            catch 
            {
                throw;
            }
        }


    }
}
