using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EurocomV2.Models
{
    public class User
    {
        private int _UserID;
        private string _FirstName;
        private string _Lastname;
        private string _Email;
        private string _Password;
        private bool _IsEmailVerified;

        public System.DateTime DateOfBirth { get; set; }

        //Could have [To-Do ]
        public System.Guid ActivationCode { get; set; }


        public int UserID
        {
            get { return this._UserID; }
            set { _UserID = value; }
        }

        public string FirstName
        {
            get { return this._FirstName; }
            set { _FirstName = value; }
        }

        public string LastName
        {
            get { return this._Lastname; }
            set { _Lastname = value; }
        }

        public string EmailID
        {
            get { return this._Email; }
            set { _Email = value; }
        }

        public string Password
        {
            get { return this._Password; }
            set { _Password = value; }
        }

        public bool IsEmailVerified
        {
            get { return this._IsEmailVerified; }
            set { _IsEmailVerified = value; }
        }

        public string ConfirmPassword { get; internal set; }

    }
}
