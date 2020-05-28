using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class User
    {
        public int UserID { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string Role { get; private set; }

        public User(int userID, string firstName, string lastName, string role)
        {
            UserID = userID;
            FirstName = firstName;
            LastName = lastName;
            Role = role;
            
        }

        public User(int userID, string firstName, string lastName, string email, string role)
        {
            UserID = userID;
            FirstName = firstName;
            LastName = lastName;
            email = Email;
            Role = role;

        }

    }
}
