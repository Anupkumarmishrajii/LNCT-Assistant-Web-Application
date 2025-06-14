using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using DTO;
using Microsoft.Data.SqlClient;

namespace DAL
{
    public sealed class UsersDAL
    {
        internal UsersDAL() { }

        public async Task<UsersDTO> InsertUser(UsersDTO objUsersDTO)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection("Data Source=MISHRAJI\\SQLEXPRESS;Initial Catalog=WebLNCTAssistant;Integrated Security=True;Encrypt=True;Trust Server Certificate=True"))
                {
                    await connection.OpenAsync();

                    string sqlQuery = $@"IF NOT EXISTS(SELECT 1 FROM Users 
                                        WHERE Users.Mobile = @Mobile)
                                        BEGIN
                                         INSERT INTO Users
                                         (
                                         Name
                                         ,Email
                                         ,IsLNCTIan
                                         ,As_a
                                         ,HigherEaducation
                                         ,Mobile
                                         ,Status
                                         )
                                         SELECT 
                                         @Name
                                         ,@Email
                                         ,@IsLNCTIan
                                         ,@As_a
                                         ,@HigherEaducation
                                         ,@Mobile
                                         ,@Status
                                        END
                                        UPDATE Users
                                        SET
                                        Name = @Name
                                        ,Email = @Email
                                        ,IsLNCTIan = @IsLNCTIan
                                        ,As_a = @As_a
                                        ,HigherEaducation = @HigherEaducation
                                        ,Mobile = @Mobile
                                        ,Status = @Status
                                        WHERE Users.Mobile = @Mobile;
                                        ";
                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        // command.Parameters.AddWithValue("Name", objUsersDTO.Name);
                        command.Parameters.AddWithValue("Name", objUsersDTO.Name != null ? objUsersDTO.Name : DBNull.Value);
                        command.Parameters.AddWithValue("Email", objUsersDTO.Email);
                        command.Parameters.AddWithValue("IsLNCTIan", objUsersDTO.IsLNCTIan);
                        command.Parameters.AddWithValue("As_a", objUsersDTO.As_a);
                        command.Parameters.AddWithValue("HigherEaducation", objUsersDTO.HigherEaducation);
                        command.Parameters.AddWithValue("Mobile", objUsersDTO.Mobile);
                        command.Parameters.AddWithValue("Status", objUsersDTO.Status);

                      
                        int rowAfftecte = await command.ExecuteNonQueryAsync();
                        if (rowAfftecte > 0)
                        {
                            objUsersDTO.Done = true;
                        }
                        else
                        {
                            objUsersDTO.Done = false;
                        }

                    }
                    await connection.CloseAsync();
                    await connection.DisposeAsync();
                }
                return objUsersDTO;
            }
            catch
            {
                throw;
            }
        }



        public async Task<List<UsersDTO>> GetAllUsers()
        {
            try
            {
                UsersDTO? objUsersDTO = null;
                List<UsersDTO> lstUsersDTO = new List<UsersDTO>();
                using (SqlConnection connection = new SqlConnection("Data Source=MISHRAJI\\SQLEXPRESS;Initial Catalog=WebLNCTAssistant;Integrated Security=True;Encrypt=True;Trust Server Certificate=True"))
                {
                    await connection.OpenAsync();
                    string sqlQuery = $@"Select
                                         Users.UserID
                                        ,Users.Name
                                        ,Users.Email
                                        ,Users.IsLNCTIan
										,Users.As_a
										,Users.HigherEaducation
										,Users.Mobile
                                        ,Users.Status
                                        FROM Users WITH(NOLOCK)
                                        WHERE Status = 'PENDING' ;
                                            ";
                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                objUsersDTO = new UsersDTO();
                                objUsersDTO.UsersID = reader.GetInt64(reader.GetOrdinal("UserID"));
                                objUsersDTO.Name = reader.GetString(reader.GetOrdinal("Name"));
                                objUsersDTO.Email = reader.GetString(reader.GetOrdinal("Email"));
                                objUsersDTO.IsLNCTIan = reader.GetInt32(reader.GetOrdinal("IsLNCTIan"));
                                objUsersDTO.As_a = reader.GetString(reader.GetOrdinal("As_a"));
                                objUsersDTO.HigherEaducation = reader.GetString(reader.GetOrdinal("HigherEaducation"));
                                objUsersDTO.Mobile = reader.GetString(reader.GetOrdinal("Mobile"));
                                objUsersDTO.Status = reader.GetString(reader.GetOrdinal("Status"));
                                lstUsersDTO.Add(objUsersDTO);
                            }
                        }
                    }
                    await connection.CloseAsync();
                    await connection.DisposeAsync();
                }

                return lstUsersDTO;
            }
            catch
            {
                throw;
            }
        }


        public async Task<UsersDTO> GetUserByID(long userid)
        {
            try
            {
                UsersDTO? objUsersDTO = null;
                using (SqlConnection connection = new SqlConnection("Data Source=MISHRAJI\\SQLEXPRESS;Initial Catalog=WebLNCTAssistant;Integrated Security=True;Encrypt=True;Trust Server Certificate=True"))
                {
                    await connection.OpenAsync();

                    string sqlQuery = $@"Select
                                         Users.UserID
                                        ,Users.Name
                                        ,Users.Email
                                        ,Users.IsLNCTIan
										,Users.As_a
										,Users.HigherEaducation
										,Users.Mobile
                                        ,Users.Status
                                        FROM Users WITH(NOLOCK)
                                        WHERE Status = 'PENDING' 
										AND UserID = @UserID;
                                            ";

                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        command.Parameters.AddWithValue("UserID", userid);
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                objUsersDTO = new UsersDTO();
                                objUsersDTO.UsersID = reader.GetInt64(reader.GetOrdinal("UserID"));
                                objUsersDTO.Name = reader.GetString(reader.GetOrdinal("Name"));
                                objUsersDTO.Email = reader.GetString(reader.GetOrdinal("Email"));
                                objUsersDTO.IsLNCTIan = reader.GetInt32(reader.GetOrdinal("IsLNCTIan"));
                                objUsersDTO.As_a = reader.GetString(reader.GetOrdinal("As_a"));
                                objUsersDTO.HigherEaducation = reader.GetString(reader.GetOrdinal("HigherEaducation"));
                                objUsersDTO.Mobile = reader.GetString(reader.GetOrdinal("Mobile"));
                                objUsersDTO.Status = reader.GetString(reader.GetOrdinal("Status"));
                            }
                        }
                    }
                }

                return objUsersDTO;
            }
            catch
            {
                throw;
            }
        }


        public async Task<UsersDTO> UpdateUser(UsersDTO objUsersDTO)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection("Data Source=MISHRAJI\\SQLEXPRESS;Initial Catalog=WebLNCTAssistant;Integrated Security=True;Encrypt=True;Trust Server Certificate=True"))
                {
                    await connection.OpenAsync();

                    string sqlQuery = $@"UPDATE Users
                                                SET
                                                Name = @Name
                                                ,Email = @Email
                                                ,IsLNCTIan = @IsLNCTIan
                                                ,As_a = @As_a
                                                ,HigherEaducation = @HigherEaducation
                                                ,Mobile = @Mobile
                                                ,Status = @Status
                                                WHERE UserID = @UserID;
                                                ";

                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        command.Parameters.AddWithValue("Name", objUsersDTO.Name);
                        command.Parameters.AddWithValue("Email", objUsersDTO.Email);
                        command.Parameters.AddWithValue("IsLNCTIan", objUsersDTO.IsLNCTIan);
                        command.Parameters.AddWithValue("As_a", objUsersDTO.As_a);
                        command.Parameters.AddWithValue("HigherEaducation", objUsersDTO.HigherEaducation);
                        command.Parameters.AddWithValue("Mobile", objUsersDTO.Mobile);
                        command.Parameters.AddWithValue("Status", objUsersDTO.Status);
                        command.Parameters.AddWithValue("UserID", objUsersDTO.UsersID);

                        int rowAfftecte = await command.ExecuteNonQueryAsync();
                        if (rowAfftecte > 0)
                        {
                            objUsersDTO.Done = true;
                        }
                        else
                        {
                            objUsersDTO.Done = false;
                        }
                    }

                    await connection.CloseAsync();
                    await connection.DisposeAsync();
                }
                return objUsersDTO;
            }
            catch
            {
                throw;
            }
        }

    }
}
