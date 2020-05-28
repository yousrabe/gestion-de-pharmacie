using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using DataAccessLayer;
using DataObjects;

namespace LogicLayer
{
    public  class UserManager
    {

        // need to authenticate the user
        public User AuthenticateUser(string email, string password)
        {
            // this needs to be changed to return a user object
            User user = null;

            // hash the password
            password = hashSHA256(password);

            // this is unsafe code...
            try
            {
               // Check is a user exist with such email
                    user = UserAccessor.RetrieveUserByEmail(email);
                if (user != null)
                {
                    if (UserAccessor.VerifyPassword(email, password))
                    {  // Check if the user is new

                        return user;

                    }

                    else
                        throw new ApplicationException("Wrong Password ! Please Try again");
                        
                }
                else
                {
                    throw new ApplicationException("User not found ! try a different Email");
                }


                // note: create a user object if this worked
            }
            catch (Exception ex) // we decide what to do with exceptions
            {
                throw new ApplicationException(ex.Message);
            }

        
        }


        // method to allow a password to be changed
        public bool UpdatePassword(string username, string oldPassword, string newPassword)
        {
            bool result = false;

            newPassword = hashSHA256(newPassword);
            oldPassword = hashSHA256(oldPassword);

            try
            {
                result = (1 == UserAccessor.UpdatePasswordHash(username, oldPassword, newPassword));
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }


        public bool FindUser(string email)
        {
            bool result = false;

            try
            {
                result = UserAccessor.RetrieveUserByEmail(email) != null;
            }
            catch (Exception)
            {
                result = false;
            }

            return result;
        }


        // need to hash the password
        private string hashSHA256(string source)
        {
            string result = "";

            // we need a byte array
            byte[] data;

            // use a .NET hash provider
            using (SHA256 sha256hash = SHA256.Create())
            {
                // hash the input
                data = sha256hash.ComputeHash(Encoding.UTF8.GetBytes(source));
            }

            // now, we just need to build the result string
            var s = new StringBuilder();
            // loop through the bytes creating hex digits
            for (int i = 0; i < data.Length; i++)
            {
                s.Append(data[i].ToString("x2"));
            }

            // convert the stringbuilder to a string
            result = s.ToString();

            return result;
        }
    }
}
