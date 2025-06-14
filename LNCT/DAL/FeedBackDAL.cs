using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using Microsoft.Data.SqlClient;

namespace DAL
{
    public sealed class FeedBackDAL
    {
        internal FeedBackDAL() { }

        public async Task<FeedBackDTO> InsertFeedBack(FeedBackDTO objFeedBackDTO)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection("Data Source=MISHRAJI\\SQLEXPRESS;Initial Catalog=WebLNCTAssistant;Integrated Security=True;Encrypt=True;Trust Server Certificate=True"))
                {
                    await connection.OpenAsync();

                    string sqlQuery = $@"IF NOT EXISTS(SELECT 1 FROM FeedBack WITH(NOLOCK)
                                    WHERE  FeedBack.FeedBackType = @FeedBackType AND LOWER(FeedBack.Subject) = LOWER(@Subject))  
                                    BEGIN
                                    INSERT INTO FeedBack
                                    (
                                    Name
                                    ,Email
                                    ,Mobile
                                    ,FeedBackType
                                    ,Subject
                                    ,FeedBackMessage
                                    ,Priority_Level
                                    ,Status
                                    )
                                    SELECT 
                                    @Name
                                    ,@Email
                                    ,@Mobile
                                    ,@FeedBackType
                                    ,@Subject
                                    ,@FeedBackMessage
                                    ,@Priority_Level
                                    ,@Status
                                    END 
                                    UPDATE FeedBack
                                     SET
                                    Name = @Name
                                    ,Email = @Email
                                    ,Mobile = @Mobile
                                    ,FeedBackType = @FeedBackType
                                    ,Subject = @Subject
                                    ,FeedBackMessage = @FeedBackMessage
                                    ,Priority_Level = @Priority_Level
                                    ,Status = @Status
                                    WHERE FeedBack.FeedBackType = @FeedBackType AND LOWER(FeedBack.Subject) = LOWER(@Subject);
                                    ";
                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        command.Parameters.AddWithValue("Name", objFeedBackDTO.Name != null ? objFeedBackDTO.Name : DBNull.Value);
                        command.Parameters.AddWithValue("Email", objFeedBackDTO.Email);
                        command.Parameters.AddWithValue("Mobile", objFeedBackDTO.Mobile);
                        command.Parameters.AddWithValue("FeedBackType", objFeedBackDTO.FeedBackType);
                        command.Parameters.AddWithValue("Subject", objFeedBackDTO.Subject);
                        command.Parameters.AddWithValue("FeedBackMessage", objFeedBackDTO.FeedBackMessage);
                        command.Parameters.AddWithValue("Priority_Level", objFeedBackDTO.Priority_Level);
                        command.Parameters.AddWithValue("Status", objFeedBackDTO.Status);

                        //using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        //{
                        //    if (await reader.ReadAsync())
                        //    {
                        //        if (!reader.IsDBNull(0))
                        //        {
                        //            long newFeedBackID = Convert.ToInt64(reader["FeedBackID"]);

                        //            if (newFeedBackID > 0)
                        //            {
                        //                objFeedBackDTO.Done = true;
                        //            }
                        //            else
                        //            {
                        //                objFeedBackDTO.Done = false;
                        //            }
                        //        }
                        //    }

                        //}
                        int rowAffected = await command.ExecuteNonQueryAsync();
                        if (rowAffected > 0)
                        {
                            objFeedBackDTO.Done = true;
                        }
                        else
                        {
                            objFeedBackDTO.Done = false;
                        }
                    }
                    await connection.CloseAsync();
                    await connection.DisposeAsync();
                }
                return objFeedBackDTO;
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<FeedBackDTO>> GetAllFeedBack()
        {
            try
            {
                 FeedBackDTO? objFeedBackDTO = null;
            List<FeedBackDTO> lstFeedBackDTO = new List<FeedBackDTO> ();
            using (SqlConnection connection = new SqlConnection("Data Source=MISHRAJI\\SQLEXPRESS;Initial Catalog=WebLNCTAssistant;Integrated Security=True;Encrypt=True;Trust Server Certificate=True"))
            {
                await connection.OpenAsync();

                string sqlQuery = $@"SELECT 
                                        FeedBack.FeedBackID
                                        ,FeedBack.Name
                                        ,FeedBack.Email
                                        ,FeedBack.Mobile
                                        ,FeedBack.FeedBackType
                                        ,FeedBack.Subject
                                        ,FeedBack.FeedBackMessage
                                        ,FeedBack.Priority_Level
                                        ,FeedBack.Status
                                        FROM FeedBack WITH(NOLOCK)
                                        WHERE Status = 'PENDING';
                                        ";
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            objFeedBackDTO = new FeedBackDTO();
                            objFeedBackDTO.FeedBackID = reader.GetInt64(reader.GetOrdinal("FeedBackID"));
                            objFeedBackDTO.Name = reader.GetString(reader.GetOrdinal("Name"));
                            objFeedBackDTO.Email = reader.GetString(reader.GetOrdinal("Email"));
                            objFeedBackDTO.Mobile = reader.GetString(reader.GetOrdinal("Mobile"));
                            objFeedBackDTO.FeedBackType = reader.GetString(reader.GetOrdinal("FeedBackType"));
                            objFeedBackDTO.Subject = reader.GetString(reader.GetOrdinal("Subject"));
                            objFeedBackDTO.FeedBackMessage = reader.GetString(reader.GetOrdinal("FeedBackMessage"));
                            objFeedBackDTO.Priority_Level = reader.GetString(reader.GetOrdinal("Priority_Level"));
                            objFeedBackDTO.Status = reader.GetString(reader.GetOrdinal("Status"));
                            lstFeedBackDTO.Add(objFeedBackDTO);
                        }
                    }
                }
                await connection.CloseAsync();
                await connection.DisposeAsync();
            }

            return lstFeedBackDTO;
            }
            catch
            {
                throw;
            }
        }

        public async Task<FeedBackDTO> GetFeedBackByID(long id)
        {
            try
            {
                FeedBackDTO objFeedBackDTO = null;
                using (SqlConnection connection = new SqlConnection("Data Source=MISHRAJI\\SQLEXPRESS;Initial Catalog=WebLNCTAssistant;Integrated Security=True;Encrypt=True;Trust Server Certificate=True"))
                {
                    await connection.OpenAsync();

                    string sqlQuery = $@" SELECT 
                                        FeedBack.FeedBackID
                                        ,FeedBack.Name
                                        ,FeedBack.Email
                                        ,FeedBack.Mobile
                                        ,FeedBack.FeedBackType
                                        ,FeedBack.Subject
                                        ,FeedBack.FeedBackMessage
                                        ,FeedBack.Priority_Level
                                        ,FeedBack.Status
                                        FROM FeedBack WITH(NOLOCK)
                                        WHERE Status = 'PENDING'
										AND FeedBackID = @FeedBackID;
                                                ";
                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        command.Parameters.AddWithValue("FeedBackID", id);

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                objFeedBackDTO = new FeedBackDTO();
                                objFeedBackDTO.FeedBackID = reader.GetInt64(reader.GetOrdinal("FeedBackID"));
                                objFeedBackDTO.Name = reader.GetString(reader.GetOrdinal("Name"));
                                objFeedBackDTO.Email = reader.GetString(reader.GetOrdinal("Email"));
                                objFeedBackDTO.Mobile = reader.GetString(reader.GetOrdinal("Mobile"));
                                objFeedBackDTO.FeedBackType = reader.GetString(reader.GetOrdinal("FeedBackType"));
                                objFeedBackDTO.Subject = reader.GetString(reader.GetOrdinal("Subject"));
                                objFeedBackDTO.FeedBackMessage = reader.GetString(reader.GetOrdinal("FeedBackMessage"));
                                objFeedBackDTO.Priority_Level = reader.GetString(reader.GetOrdinal("Priority_Level"));
                                objFeedBackDTO.Status = reader.GetString(reader.GetOrdinal("Status"));
                            }
                        }
                    }
                    await connection.CloseAsync();
                    await connection.DisposeAsync();
                }

                return objFeedBackDTO;
            }
            catch
            {
                throw;
            }
        }

        public async Task<FeedBackDTO> UpdateFeedBack(FeedBackDTO objFeedBackDTO)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection("Data Source=MISHRAJI\\SQLEXPRESS;Initial Catalog=WebLNCTAssistant;Integrated Security=True;Encrypt=True;Trust Server Certificate=True"))
                {
                    await connection.OpenAsync();
                    string sqlQuery = $@"UPDATE FeedBack
                                                    SET
                                                    Name = @Name
                                                    ,Email = @Email
                                                    ,Mobile = @Mobile
                                                    ,FeedBackType = @FeedBackType
                                                    ,Subject = @Subject
                                                    ,FeedBackMessage = @FeedBackMessage
                                                    ,Priority_Level = @Priority_Level
                                                    ,Status = @Status
                                                    WHERE FeedBackID = @FeedBackID;
                                                    ";
                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        command.Parameters.AddWithValue("Name", objFeedBackDTO.Name != null ? objFeedBackDTO.Name : DBNull.Value);
                        command.Parameters.AddWithValue("Email", objFeedBackDTO.Email);
                        command.Parameters.AddWithValue("Mobile", objFeedBackDTO.Mobile);
                        command.Parameters.AddWithValue("FeedBackType", objFeedBackDTO.FeedBackType);
                        command.Parameters.AddWithValue("Subject", objFeedBackDTO.Subject);
                        command.Parameters.AddWithValue("FeedBackMessage", objFeedBackDTO.FeedBackMessage);
                        command.Parameters.AddWithValue("Priority_Level", objFeedBackDTO.Priority_Level);
                        command.Parameters.AddWithValue("Status", objFeedBackDTO.Status);
                        command.Parameters.AddWithValue("FeedBackID", objFeedBackDTO.FeedBackID);

                       int rowAffected = await command.ExecuteNonQueryAsync();
                        if (rowAffected > 0)
                        {
                            objFeedBackDTO.Done = true;
                        }
                        else
                        {
                            objFeedBackDTO.Done = false;
                        }
                    }
                    await connection.CloseAsync();
                    await connection.DisposeAsync();
                }
                return objFeedBackDTO;
            }
            catch
            {
                throw;
            }
        }
    }
}
