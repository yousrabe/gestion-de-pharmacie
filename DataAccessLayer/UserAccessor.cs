using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessLayer
{
    public static class UserAccessor
    {

        public static bool VerifyPassword(string email, string password)
        { 
            Object  result = null ;// result is null if password is wrong 
            // we begin with a connection
            var conn = DBConnection.GetDbConnection();

            // next, we need command text
            string cmdText = @"sp_verify_password";

            // we create a command object from command text and a connection
            var cmd = new SqlCommand(cmdText, conn);

            // now we need to set the command type
            cmd.CommandType = CommandType.StoredProcedure;

            // next, set up the stored procedure's parameters
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@Password", SqlDbType.NVarChar, 100);
            // pass values to the parameters
            cmd.Parameters["@Email"].Value = email;
            cmd.Parameters["@Password"].Value = password;

            // now we need to use these connect types in an open connection,
            // and this means unsafe code, so a try-catch
            try
            {
                // open the connection
                conn.Open();

                // execute the command
               result = cmd.ExecuteScalar();
            }
            catch (Exception up)
            {
                throw up; // never handle expection at this layer, bubble them up
            }
            if (result == null)
            return false;
            else 
                return true ;
        }

        public static User RetrieveUserByEmail(string email)
        {
            User user = null;

            // get a connection
            var conn = DBConnection.GetDbConnection();

            // command text
            string cmdText = @"sp_retrieve_user_by_email";

            // command objects
            var cmd = new SqlCommand(cmdText, conn);

            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 250);

            // values
            cmd.Parameters["@Email"].Value = email;

            try
            {
                int userID = 0;
                string firstName = null;
                string lastName = null;
                string role = null;
                // open the connection
                conn.Open();

                // process cmd1
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read(); // reads the first line
                    userID = reader.GetInt32(0);
                    firstName = reader.GetString(1);
                    lastName = reader.GetString(2);
                    role = reader.GetString(3);
                    // build a user object to be returned
                    user = new User(userID, firstName, lastName, role);
                }
                else
                {
                    throw new ApplicationException("User not found.");
                   
                }
                reader.Close(); // only 1 reader can be open at a time

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return user;
        }

        


        public static int UpdatePasswordHash(string email,  string oldPassword, string newPassword)
        {
            int result = 0;

            // connection
            var conn = DBConnection.GetDbConnection();

            // command text
            string cmdText = "sp_update_password_hash";

            // command
            var cmd = new SqlCommand(cmdText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@OldPasswordHash", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@NewPasswordHash", SqlDbType.NVarChar, 100);
            
            // values
            cmd.Parameters["@Email"].Value = email;
            cmd.Parameters["@OldPasswordHash"].Value = oldPassword;
            cmd.Parameters["@NewPasswordHash"].Value = newPassword;

            // execute the command
            try
            {
                // open the connection
                conn.Open();

                // execute the command
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return result;
        }

        public static int UpdateEmployeeInfo(int employeeID, string oldEmail, string newEmail, string oldFirstName, string newFirstName, string oldLastName, string newLastName, string oldPassword, string newPassword, string oldPhoneNumber, string newPhoneNumber, string oldRole, string newRole)
        {
            int result = 0;

            // connection
            var conn = DBConnection.GetDbConnection();

            // command text
            string cmdText = "sp_update_employee_info";

            // command
            var cmd = new SqlCommand(cmdText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd.Parameters.Add("@EmployeeID", SqlDbType.Int);
            cmd.Parameters.Add("@OldEmail", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@NewEmail", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@OldFirstName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@NewFirstName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@OldLastName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@NewLastName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@OldRole", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@NewRole", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@OldPhoneNumber", SqlDbType.NVarChar, 11);
            cmd.Parameters.Add("@NewPhoneNumber", SqlDbType.NVarChar, 11);
            cmd.Parameters.Add("@OldPasswordHash", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@NewPasswordHash", SqlDbType.NVarChar, 100);

            // values
            cmd.Parameters["@EmployeeID"].Value = employeeID;
            cmd.Parameters["@OldEmail"].Value = oldEmail;
            cmd.Parameters["@NewEmail"].Value = newEmail;
            cmd.Parameters["@OldFirstName"].Value = oldFirstName;
            cmd.Parameters["@NewFirstName"].Value = newFirstName;
            cmd.Parameters["@OldLastName"].Value = oldLastName;
            cmd.Parameters["@NewLastName"].Value = newLastName;
            cmd.Parameters["@OldRole"].Value = oldRole;
            cmd.Parameters["@NewRole"].Value = newRole;
            cmd.Parameters["@OldPhoneNumber"].Value = oldPhoneNumber;
            cmd.Parameters["@NewPhoneNumber"].Value = newPhoneNumber;
            cmd.Parameters["@OldPasswordHash"].Value = oldPassword;
            cmd.Parameters["@NewPasswordHash"].Value = newPassword;

            // execute the command
            try
            {
                // open the connection
                conn.Open();

                // execute the command
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return result;
        }
    }
}
