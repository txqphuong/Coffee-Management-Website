using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace QuanLyCaPhe.Models
{
    public class User
    {
        //fields
        private string username;
        private string password = string.Empty;
        private bool rememberPass;

        //properties
        [Required(ErrorMessage = "This field must be filled"),
        StringLength(50, ErrorMessage = "Please enter username less than 50 characters")]
        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        [Required(ErrorMessage = "This field must be filled"), 
        MinLength(5, ErrorMessage = "Password must has more than 5 characters"), MaxLength(255, ErrorMessage = "Password must has less than 255 charaters")]
        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public bool RememberPass
        {
            get { return rememberPass; }
            set { rememberPass = value; }
        }

        //constructor
        public User()
        {

        }

        public User(string username, string password)
        {
            this.username = username;
            this.password = password;
            this.rememberPass = true;
        }

        public User(User another)
        {
            this.username = another.Username;
            this.password = another.Password;
            this.rememberPass = another.RememberPass;
        }
    }
}