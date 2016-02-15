using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankCardsServerTokenization
{
    // class that represent User with username and password fields
    public class Users
    {
        #region Fields
        private string username;
        private string password;
        #endregion

        #region Properties
        public string Username
        {
            get
            {
                return username;
            }
            set
            {
                if (value != null)
                {
                    username = value;
                }
            }
        }
        public string Password
        {
            get
            {
                return password;
            }
            private set
            {
                if (value != null)
                {
                    password = value;
                }
            }
        }
        #endregion

        #region Constructors
        // Constructor with two parameters
        public Users(string username, string password)
        {
            Username = username;
            Password = password;
        }
        // Default constructor
        public Users():this("error", "error") { }
        // Copy constructor
        public Users(Users user) : this(user.Username, user.Password) { }
        #endregion
    }
}
