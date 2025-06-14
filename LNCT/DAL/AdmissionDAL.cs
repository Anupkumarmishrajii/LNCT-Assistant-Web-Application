using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Http;

namespace DAL
{
    public sealed class AdmissionDAL
    {
        internal AdmissionDAL() { }


        public async Task<AdmissionDTO> InsertAdmission(AdmissionDTO objAdmissionDTO)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection("Data Source=MISHRAJI\\SQLEXPRESS;Initial Catalog=WebLNCTAssistant;Integrated Security=True;Encrypt=True;Trust Server Certificate=True"))
                {
                    await connection.OpenAsync();
                    string sqlQuery = $@"IF NOT EXISTS(SELECT 1 FROM Admission WITH(NOLOCK)
                                         WHERE Admission.Mobile = @Mobile)
                                         BEGIN
                                                   INSERT INTO Admission
                                                    (
                                                    Name
                                                    ,Email
                                                    ,DOB
                                                    ,Gender
                                                    ,Disability
                                                    ,Address
                                                    ,Mobile
                                                    ,AlternateMobile
                                                    ,Previous_Institute
                                                    ,Year_PassedOut
                                                    ,Score
                                                    ,Desired_Program
                                                    ,Language
                                                    ,Reference
                                                    ,Status
                                                    )
                                                    SELECT 
                                                    @Name
                                                    ,@Email
                                                    ,@DOB
                                                    ,@Gender
                                                    ,@Disability
                                                    ,@Address
                                                    ,@Mobile
                                                    ,@AlternateMobile
                                                    ,@Previous_Institute
                                                    ,@Year_PassedOut
                                                    ,@Score
                                                    ,@Desired_Program
                                                    ,@Language
                                                    ,@Reference
                                                    ,@Status
                                                  
                                         END 
                                         UPDATE Admission
                                         SET
                                         Name = @Name
                                         ,Email = @Email
                                         ,DOB = @DOB
                                         ,Gender = @Gender
                                         ,Disability = @Disability
                                         ,Address = @Address
                                         ,Mobile = @Mobile
                                         ,AlternateMobile = @AlternateMobile
                                         ,Previous_Institute = @Previous_Institute
                                         ,Year_PassedOut = @Year_PassedOut
                                         ,Score = @Score
                                         ,Desired_Program = @Desired_Program
                                         ,Language = @Language
                                         ,Reference = @Reference
                                         ,Status = @Status
                                         WHERE Admission.Mobile = @Mobile;
                                        ";
                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        command.Parameters.AddWithValue("Name", objAdmissionDTO.Name);
                        command.Parameters.AddWithValue("Email", objAdmissionDTO.Email);
                        command.Parameters.AddWithValue("DOB", objAdmissionDTO.DOB);
                        command.Parameters.AddWithValue("Gender", objAdmissionDTO.Gender);
                        command.Parameters.AddWithValue("Disability", objAdmissionDTO.Disability);
                        command.Parameters.AddWithValue("Address", objAdmissionDTO.Address);
                        command.Parameters.AddWithValue("Mobile", objAdmissionDTO.Mobile);
                        command.Parameters.AddWithValue("AlternateMobile", objAdmissionDTO.AlternateMobile != null ? objAdmissionDTO.AlternateMobile : DBNull.Value);
                        command.Parameters.AddWithValue("Previous_Institute", objAdmissionDTO.Previous_Institute);
                        command.Parameters.AddWithValue("Year_PassedOut", objAdmissionDTO.Year_PassedOut);
                        command.Parameters.AddWithValue("Score", objAdmissionDTO.Score);
                        command.Parameters.AddWithValue("Desired_Program", objAdmissionDTO.Desired_Program);
                        command.Parameters.AddWithValue("Language", objAdmissionDTO.Language);
                        command.Parameters.AddWithValue("Reference", objAdmissionDTO.Reference != null ? objAdmissionDTO.Reference : DBNull.Value);
                        command.Parameters.AddWithValue("Status", objAdmissionDTO.Status);

                        int rowAffected = await command.ExecuteNonQueryAsync();
                        if (rowAffected > 0)
                        {
                            objAdmissionDTO.Done = true;
                        }
                        else
                        {
                            objAdmissionDTO.Done = false;
                        }


                    }

                    await connection.CloseAsync();
                    await connection.DisposeAsync();
                }
                return objAdmissionDTO;
            }
            catch
            {
                throw;
            }
        }


        public async Task<List<AdmissionDTO>> GetAllAdmission()
        {
            AdmissionDTO objAdmissionDTO = null;
            List<AdmissionDTO> lstAdmissionDTO = new List<AdmissionDTO>();

            try
            {
                using (SqlConnection connection = new SqlConnection("Data Source=MISHRAJI\\SQLEXPRESS;Initial Catalog=WebLNCTAssistant;Integrated Security=True;Encrypt=True;Trust Server Certificate=True"))
                {
                    await connection.OpenAsync();

                    string sqlQuery = $@"SELECT 
                                        Admission.AdmissionID
                                        ,Admission.Name
                                        ,Admission.Email
                                        ,Admission.DOB
                                        ,Admission.Gender
                                        ,Admission.Disability
                                        ,Admission.Address
                                        ,Admission.Mobile
                                        ,Admission.AlternateMobile
                                        ,Admission.Previous_Institute
                                        ,Admission.Year_PassedOut
                                        ,Admission.Score
                                        ,Admission.Desired_Program
                                        ,Admission.Language
                                        ,Admission.Reference
                                        ,Admission.Status
                                        FROM Admission WITH(NOLOCK)
                                        WHERE Status = 'PENDING';
                                        ";

                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                objAdmissionDTO = new AdmissionDTO();
                                objAdmissionDTO.AdmissionID = reader.GetInt64(reader.GetOrdinal("AdmissionID"));
                                objAdmissionDTO.Name = reader.GetString(reader.GetOrdinal("Name"));
                                objAdmissionDTO.Email = reader.GetString(reader.GetOrdinal("Email"));
                                objAdmissionDTO.DOB = reader.GetDateTime(reader.GetOrdinal("DOB"));
                                objAdmissionDTO.Gender = reader.GetString(reader.GetOrdinal("Gender"));
                                objAdmissionDTO.Disability = reader.GetInt32(reader.GetOrdinal("Disability"));
                                objAdmissionDTO.Address = reader.GetString(reader.GetOrdinal("Address"));
                                objAdmissionDTO.Mobile = reader.GetString(reader.GetOrdinal("Mobile"));

                                string alternateMobileKey = "AlternateMobile";
                                int alternateMobileOrdinal = reader.GetOrdinal(alternateMobileKey);
                                if (!reader.IsDBNull(alternateMobileOrdinal))
                                {
                                    objAdmissionDTO.AlternateMobile = reader.GetString(reader.GetOrdinal("AlternateMobile"));
                                }
                                else
                                {
                                    // If the value is null, you can assign a default value or leave it empty
                                    objAdmissionDTO.AlternateMobile = ""; // or whatever default value you prefer
                                }
                                objAdmissionDTO.Previous_Institute = reader.GetString(reader.GetOrdinal("Previous_Institute"));
                                objAdmissionDTO.Year_PassedOut = reader.GetInt32(reader.GetOrdinal("Year_PassedOut"));
                                objAdmissionDTO.Score = reader.GetString(reader.GetOrdinal("Score"));
                                objAdmissionDTO.Desired_Program = reader.GetString(reader.GetOrdinal("Desired_Program"));
                                objAdmissionDTO.Language = reader.GetString(reader.GetOrdinal("Language"));

                                string Referencekey = "Reference";
                                int ReferencekeyOrdinal = reader.GetOrdinal(Referencekey);
                                if (!reader.IsDBNull(ReferencekeyOrdinal))
                                {
                                    objAdmissionDTO.Reference = reader.GetString(reader.GetOrdinal("Reference"));
                                }
                                else
                                {
                                    // If the value is null, you can assign a default value or leave it empty
                                    objAdmissionDTO.Reference = ""; // or whatever default value you prefer
                                }
                                objAdmissionDTO.Status = reader.GetString(reader.GetOrdinal("Status"));
                                lstAdmissionDTO.Add(objAdmissionDTO);
                            }
                        }
                    }
                    await connection.CloseAsync();
                    await connection.DisposeAsync();
                }

            }
            catch
            {
                throw;
            }
            return lstAdmissionDTO;
        }

        public async Task<AdmissionDTO> GetAdmissionByID(long id)
        {
            try
            {
                AdmissionDTO objAdmissionDTO = null;
                using (SqlConnection connection = new SqlConnection("Data Source=MISHRAJI\\SQLEXPRESS;Initial Catalog=WebLNCTAssistant;Integrated Security=True;Encrypt=True;Trust Server Certificate=True"))
                {
                    await connection.OpenAsync();

                    string sqlQuery = $@"SELECT 
                                        Admission.AdmissionID
                                        ,Admission.Name
                                        ,Admission.Email
                                        ,Admission.DOB
                                        ,Admission.Gender
                                        ,Admission.Disability
                                        ,Admission.Address
                                        ,Admission.Mobile
                                        ,Admission.AlternateMobile
                                        ,Admission.Previous_Institute
                                        ,Admission.Year_PassedOut
                                        ,Admission.Score
                                        ,Admission.Desired_Program
                                        ,Admission.Language
                                        ,Admission.Reference
                                        ,Admission.Status
                                        FROM Admission WITH(NOLOCK)
                                        WHERE Status = 'PENDING'
										AND AdmissionID = @AdmissionID;
                                            ";
                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        command.Parameters.AddWithValue("AdmissionID", id);
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                objAdmissionDTO = new AdmissionDTO();
                                objAdmissionDTO.AdmissionID = reader.GetInt64(reader.GetOrdinal("AdmissionID"));
                                objAdmissionDTO.Name = reader.GetString(reader.GetOrdinal("Name"));
                                objAdmissionDTO.Email = reader.GetString(reader.GetOrdinal("Email"));
                                objAdmissionDTO.DOB = reader.GetDateTime(reader.GetOrdinal("DOB"));
                                objAdmissionDTO.Gender = reader.GetString(reader.GetOrdinal("Gender"));
                                objAdmissionDTO.Disability = reader.GetInt32(reader.GetOrdinal("Disability"));
                                objAdmissionDTO.Address = reader.GetString(reader.GetOrdinal("Address"));
                                objAdmissionDTO.Mobile = reader.GetString(reader.GetOrdinal("Mobile"));

                                string alternateMobileKey = "AlternateMobile";
                                int alternateMobileOrdinal = reader.GetOrdinal(alternateMobileKey);
                                if (!reader.IsDBNull(alternateMobileOrdinal))
                                {
                                    objAdmissionDTO.AlternateMobile = reader.GetString(reader.GetOrdinal("AlternateMobile"));
                                }
                                else
                                {
                                    // If the value is null, you can assign a default value or leave it empty
                                    objAdmissionDTO.AlternateMobile = ""; // or whatever default value you prefer
                                }
                                objAdmissionDTO.Previous_Institute = reader.GetString(reader.GetOrdinal("Previous_Institute"));
                                objAdmissionDTO.Year_PassedOut = reader.GetInt32(reader.GetOrdinal("Year_PassedOut"));
                                objAdmissionDTO.Score = reader.GetString(reader.GetOrdinal("Score"));
                                objAdmissionDTO.Desired_Program = reader.GetString(reader.GetOrdinal("Desired_Program"));
                                objAdmissionDTO.Language = reader.GetString(reader.GetOrdinal("Language"));

                                string Referencekey = "Reference";
                                int ReferencekeyOrdinal = reader.GetOrdinal(Referencekey);
                                if (!reader.IsDBNull(ReferencekeyOrdinal))
                                {
                                    objAdmissionDTO.Reference = reader.GetString(reader.GetOrdinal("Reference"));
                                }
                                else
                                {
                                    // If the value is null, you can assign a default value or leave it empty
                                    objAdmissionDTO.Reference = ""; // or whatever default value you prefer
                                }
                                objAdmissionDTO.Status = reader.GetString(reader.GetOrdinal("Status"));
                            }
                        }
                    }
                }

                return objAdmissionDTO;
            }
            catch
            {
                throw;
            }
        }

        public async Task<AdmissionDTO> UpdateAdmission(AdmissionDTO objAdmissionDTO)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection("Data Source=MISHRAJI\\SQLEXPRESS;Initial Catalog=WebLNCTAssistant;Integrated Security=True;Encrypt=True;Trust Server Certificate=True"))
                {
                    await connection.OpenAsync();
                    string sqlQuery = $@"
                                        UPDATE Admission
                                        SET
                                        Name = @Name
                                        ,Email = @Email
                                        ,DOB = @DOB
                                        ,Gender = @Gender
                                        ,Disability = @Disability
                                        ,Address = @Address
                                        ,Mobile = @Mobile
                                        ,AlternateMobile = @AlternateMobile
                                        ,Previous_Institute = @Previous_Institute
                                        ,Year_PassedOut = @Year_PassedOut
                                        ,Score = @Score
                                        ,Desired_Program = @Desired_Program
                                        ,Language = @Language
                                        ,Reference = @Reference
                                        ,Status = @Status
                                        WHERE AdmissionID = @AdmissionID;
                                        ";

                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        command.Parameters.AddWithValue("Name", objAdmissionDTO.Name);
                        command.Parameters.AddWithValue("Email", objAdmissionDTO.Email);
                        command.Parameters.AddWithValue("DOB", objAdmissionDTO.DOB);
                        command.Parameters.AddWithValue("Gender", objAdmissionDTO.Gender);
                        command.Parameters.AddWithValue("Disability", objAdmissionDTO.Disability);
                        command.Parameters.AddWithValue("Address", objAdmissionDTO.Address);
                        command.Parameters.AddWithValue("Mobile", objAdmissionDTO.Mobile);
                        command.Parameters.AddWithValue("AlternateMobile", objAdmissionDTO.AlternateMobile != null ? objAdmissionDTO.AlternateMobile : DBNull.Value);
                        command.Parameters.AddWithValue("Previous_Institute", objAdmissionDTO.Previous_Institute);
                        command.Parameters.AddWithValue("Year_PassedOut", objAdmissionDTO.Year_PassedOut);
                        command.Parameters.AddWithValue("Score", objAdmissionDTO.Score);
                        command.Parameters.AddWithValue("Desired_Program", objAdmissionDTO.Desired_Program);
                        command.Parameters.AddWithValue("Language", objAdmissionDTO.Language);
                        command.Parameters.AddWithValue("Reference", objAdmissionDTO.Reference != null ? objAdmissionDTO.Reference : DBNull.Value);
                        command.Parameters.AddWithValue("Status", objAdmissionDTO.Status);
                        command.Parameters.AddWithValue("AdmissionID", objAdmissionDTO.AdmissionID);

                        int rowAffected = await command.ExecuteNonQueryAsync();
                        if (rowAffected > 0)
                        {
                            objAdmissionDTO.Done = true;
                        }
                        else
                        {
                            objAdmissionDTO.Done = false;
                        }

                    }   
                    //

                    await connection.CloseAsync();
                    await connection.DisposeAsync();
                }
                return objAdmissionDTO;
            }
            catch
            {
                throw;
            }
        }
    }
}
