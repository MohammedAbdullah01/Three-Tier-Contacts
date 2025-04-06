using System;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Security.Policy;

namespace ContactsDataAccessLayer
{
    public class clsContactDataAccess
    {
        public static bool GetFindInfoById(
                int id,
                ref string FirstName,
                ref string LastName,
                ref string Email,
                ref string Phone,
                ref string Address,
                ref DateTime DateOfBirth,
                ref int CountryID,
                ref string ImagePath)
        {
            bool isFound = false;
            SqlConnection coon = new SqlConnection(clsDataAccessSettings.ConnectionSettings);

            string query = @"Select * From Contacts Where contactid = @id ";

            SqlCommand cmd = new SqlCommand(query, coon);

            cmd.Parameters.AddWithValue("@id", id);

            try
            {
                coon.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    FirstName = reader["FirstName"]?.ToString() ?? string.Empty;
                    LastName = reader["LastName"]?.ToString() ?? string.Empty;
                    Email = reader["Email"]?.ToString() ?? string.Empty;
                    Phone = reader["Phone"]?.ToString() ?? string.Empty;
                    Address = reader["Address"]?.ToString() ?? string.Empty;

                    if (reader["DateOfBirth"] != DBNull.Value)
                        DateOfBirth = (DateTime)reader["DateOfBirth"];

                    if (reader["CountryID"] != DBNull.Value)
                        CountryID = Convert.ToInt32(reader["CountryID"]);

                    ImagePath = reader["ImagePath"]?.ToString() ?? string.Empty;

                    isFound = true;
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error : {ex.Message}");
                isFound = false;
            }
            finally
            {
                coon.Close();
            }
            return isFound;
        }


        public static int AddNewContact(
            string Firstname,
            string LastName,
            string Email,
            string Phone,
            string Address,
            DateTime DateOfBirth,
            int CountryID,
            string ImagePath)
        {
            int ContactID = -1;
            SqlConnection coon = new SqlConnection(clsDataAccessSettings.ConnectionSettings);

            string query = "Insert Into contacts " +
                "(Firstname, LastName, Email, Phone, Address, DateOfBirth, CountryID, ImagePath)" +
                "Values(@Firstname, @LastName, @Email, @Phone, @Address, @DateOfBirth, @CountryID, @ImagePath);" +
                "Select SCOPE_IDENTITY()";

            SqlCommand cmd = new SqlCommand(query, coon);

            cmd.Parameters.AddWithValue("@Firstname", Firstname);
            cmd.Parameters.AddWithValue("@LastName", LastName);
            cmd.Parameters.AddWithValue("@Email", Email);
            cmd.Parameters.AddWithValue("@Phone", Phone);
            cmd.Parameters.AddWithValue("@Address", Address);
            cmd.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            cmd.Parameters.AddWithValue("@CountryID", CountryID);

            if (ImagePath != string.Empty)
                cmd.Parameters.AddWithValue("@ImagePath", ImagePath);
            else
                cmd.Parameters.AddWithValue("@ImagePath", DBNull.Value);

            try
            {
                coon.Open();
                object result = cmd.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertId))
                {
                    ContactID = insertId;
                }

            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                coon.Close();
            }

            return ContactID;
        }

        public static int UpdateContact(
            int ContactID,
            string Firstname,
            string LastName,
            string Email,
            string Phone,
            string Address,
            DateTime DateOfBirth,
            int CountryID,
            string ImagePath)
        {
            int rowAffected = 0;
            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionSettings);

            string query = @"Update contacts Set 
                            Firstname = @Firstname,
                            LastName = @LastName,
                            Email = @Email,
                            Phone = @Phone,
                            Address = @Address,
                            DateOfBirth = @DateOfBirth,
                            CountryID = @CountryID,
                            ImagePath = @ImagePath
                            Where ContactID = @ContactID;";

            SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@ContactID", ContactID);
            cmd.Parameters.AddWithValue("@Firstname", Firstname);
            cmd.Parameters.AddWithValue("@LastName", LastName);
            cmd.Parameters.AddWithValue("@Email", Email);
            cmd.Parameters.AddWithValue("@Phone", Phone);
            cmd.Parameters.AddWithValue("@Address", Address);
            cmd.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            cmd.Parameters.AddWithValue("@CountryID", CountryID);
            cmd.Parameters.AddWithValue("@ImagePath", ImagePath);

            try
            {
                conn.Open();
                rowAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                conn.Close();
            }

            return rowAffected;
        }

        public static int DeleteContact(int ContactID)
        {
            int rowAffected = 0;
            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionSettings);

            string query = "Delete From Contacts Where ContactID = @ContactID";

            SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@ContactID", ContactID);

            try
            {
                conn.Open();
                rowAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                conn.Close();
            }
            return rowAffected;
        }

        public static DataTable GetAllContacts()
        {
            DataTable dataTable = new DataTable();

            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionSettings);

            string query = "Select * From Contacts";

            SqlCommand cmd = new SqlCommand(query, conn);

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    dataTable.Load(reader);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
            finally
            {
                conn.Close();
            }

            return dataTable;
        }

        public static bool IsContactExist(int id)
        {
            bool isExist = false;
            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionSettings);

            string query = "Select Count(*) From Contacts Where ContactID = @ContactID";

            SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@ContactID", id);


            try
            {
                conn.Open();
                object result = cmd.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int rowCount))
                {
                    isExist = (rowCount > 0);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                isExist = false;
            }
            finally
            {
                conn.Close();
            }

            return isExist;
        }
    }
}
