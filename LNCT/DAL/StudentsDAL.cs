using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using Microsoft.Data.SqlClient;

namespace DAL
{
    public sealed class StudentsDAL
    {
        internal StudentsDAL() { }

        public async Task<StudentsDTO> GetPasswordByEmail(string email)
        {
            try
            {
                StudentsDTO objStudentsDTO = null;
                using (SqlConnection connection = new SqlConnection("Data Source=MISHRAJI\\SQLEXPRESS;Initial Catalog=WebLNCTAssistant;Integrated Security=True;Encrypt=True;Trust Server Certificate=True"))
                {
                    await connection.OpenAsync();

                    string sqlQuery = $@"Select
                                        Students.StudentID
                                        ,Students.Name
                                        ,Students.Email
                                        ,Students.Password
                                        ,Students.Status
                                        FROM Students WITH(NOLOCK)
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
                                objStudentsDTO = new StudentsDTO();
                                objStudentsDTO.StudentID = reader.GetInt64(reader.GetOrdinal("StudentID"));
                                objStudentsDTO.Name = reader.GetString(reader.GetOrdinal("Name"));
                                objStudentsDTO.Email = reader.GetString(reader.GetOrdinal("Email"));
                                objStudentsDTO.Password = reader.GetString(reader.GetOrdinal("Password"));
                                objStudentsDTO.Status = reader.GetInt32(reader.GetOrdinal("Status"));
                            }
                        }
                    }
                    await connection.CloseAsync();
                    await connection.DisposeAsync();
                }

                return objStudentsDTO;
            }
            catch
            {
                throw;
            }
        }
    }
}
