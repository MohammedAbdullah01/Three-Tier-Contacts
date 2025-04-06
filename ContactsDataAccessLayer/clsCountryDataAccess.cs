using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsDataAccessLayer
{
    public static class clsCountryDataAccess
    {

        public static DataTable GetAllCountries()
        {
            DataTable dt = new DataTable();

            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionSettings);

            string query = "Select * from Countries";

            SqlCommand cmd = new SqlCommand(query , conn);

            try
            {
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    dt.Load(reader);
                }

                reader.Close();
            }
            catch (Exception ex )
            {

                Console.WriteLine($"Error : {ex.Message}");
            }
            finally
            {
                conn.Close();
            }

            return dt;
        }

        public static bool GetFindInfoByCountryId(int countryId , ref string countryName, ref string code, ref string phoneCode)
        {
            bool isFound = false;
            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionSettings);

            string query = "Select * From Countries Where CountryId = @countryId";

            SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@countryId", countryId);

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    countryName = reader["CountryName"]?.ToString() ?? string.Empty;
                    code = reader["Code"]?.ToString() ?? string.Empty;
                    phoneCode = reader["PhoneCode"]?.ToString() ?? string.Empty;

                    isFound = true;
                }
                else
                {
                    isFound = false;
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                isFound = false;
            }
            finally
            {
                conn.Close();
            }

            return isFound;
        }

        public static bool GetFindInfoByCountryName(string countryName, ref int countryId,  ref string code, ref string phoneCode)
        {
            bool isFound = false;
            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionSettings);

            string query = "Select * From Countries Where CountryName = @CountryName";

            SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@CountryName", countryName);

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    if (reader["CountryID"] != DBNull.Value)
                        countryId = Convert.ToInt32(reader["CountryID"]);

                    code = reader["Code"]?.ToString() ?? string.Empty;
                    phoneCode = reader["PhoneCode"]?.ToString() ?? string.Empty;

                    isFound = true;
                }
                else
                {
                    isFound = false;
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                isFound = false;
            }
            finally
            {
                conn.Close();
            }

            return isFound;
        }

        public static int AddNewCountry(string countryName, string code,  string phoneCode)
        {
            int countryId = -1;

            SqlConnection conn = new SqlConnection( clsDataAccessSettings.ConnectionSettings);


            string query = @"Insert Into Countries (CountryName , Code , PhoneCode) Values (@countryName , @code , @phoneCode);
                            Select SCOPE_IDENTITY()";

            SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@countryName", countryName);
            cmd.Parameters.AddWithValue("@code", code);
            cmd.Parameters.AddWithValue("@phoneCode", phoneCode);

            try
            {
                conn.Open();

                object result = cmd.ExecuteScalar();

                if(result != null && int.TryParse(result.ToString() , out int newCountryId))
                {
                    countryId = newCountryId;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                countryId = -1;
            }
            finally
            {
                conn.Close();
            }
            return countryId;
        }

        public static bool UpdateCountry(int countryId, string countryName , string code , string phoneCode)
        {
            bool isUpdated = false;

            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionSettings);

            string query = @"Update Countries Set 
                            CountryName = @CountryName,
                            Code = @Code,
                            PhoneCode = @PhoneCode
                            Where CountryID = @CountryID";

            SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@CountryName", countryName);
            cmd.Parameters.AddWithValue("@Code", code);
            cmd.Parameters.AddWithValue("@PhoneCode", phoneCode);
            cmd.Parameters.AddWithValue("@CountryID", countryId);

            try
            {
                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                isUpdated = (rowsAffected > 0);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                isUpdated = false;
            }
            finally
            {
                conn.Close();
            }

            return isUpdated;
        }        

        public static bool DeleteCountry(int countryId)
        {
            bool isDeleted = false;

            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionSettings);

            string query = "Delete From Countries Where CountryID = @CountryID";

            SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@CountryID", countryId);

            try
            {
                conn.Open();

                int rowsAffected = cmd.ExecuteNonQuery();

                isDeleted = (rowsAffected > 0);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                isDeleted = false;
            }
            finally
            {
                conn.Close();
            }
            return isDeleted;
        }

        public static bool IsCountryIdExist(int countryID)
        {
            bool IsCountryExist = false;
            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionSettings);

            string query = "Select count(*) From Countries Where CountryId = @countryID";

            SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@countryID", countryID);

            try
            {
                conn.Open();

                object result = cmd.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int row))
                {
                    IsCountryExist = (row > 0);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                IsCountryExist = false;
            }
            finally
            {
                conn.Close();
            }
            return IsCountryExist;
        }

        public static bool IsCountryNameExist(string countryName)
        {
            bool IsCountryExist = false;
            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionSettings);

            string query = "Select count(*) From Countries Where CountryName = @CountryName";

            SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@CountryName", countryName);

            try
            {
                conn.Open();

                object result = cmd.ExecuteScalar();

                if(result != null && int.TryParse(result.ToString() , out int row))
                {
                    IsCountryExist = (row > 0);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                IsCountryExist = false;
            }
            finally
            {
                conn.Close();
            }
            return IsCountryExist;
        }

    }
}