using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EurocomV2.Models
{
    public class User
    {
        private int _UserId;
        private string _FirstName;
        private string _LastName;
        private string _Username;
        private string _Email;
        private string _Password;
        private string _PhoneNumber;
        private bool _Agreement;


        public int UserId
        {
            get { return this._UserId; }
            set { _UserId = value; }
        }

        public string FirstName
        {
            get { return this._FirstName; }
            set { _FirstName = value; }
        }

        public string Lastname
        {
            get { return this._LastName; }
            set { _LastName = value; }
        }

        public string Username
        {
            get { return this._Username; }
            set { _Username= value; }
        }

        public string Email
        {
            get { return this._Email; }
            set { _Email = value; }
        }

        public string Password
        {
            get { return this._Password; }
            set { _Password = value; }
        }

        public string PhoneNumber
        {
            get { return this._PhoneNumber; }
            set { _PhoneNumber = value; }
        }

        public bool Agreement
        {
            get { return this._Agreement; }
            set { _Agreement = value; }
        }
    }
}
