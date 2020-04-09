using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EurocomV2.Models
{
    public class AdminCRUD
    {
        private int _ID;
        private string _FirstName;
        private string _LastName;
        private string _Specialty;
        private string _Email;


        public int ID
        {
            get { return this._ID; }
            set { _ID = value; }
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

        public string Specialty
        {
            get { return this._Specialty; }
            set { _Specialty = value; }
        }

        public string Email
        {
            get { return this._Email; }
            set { _Email = value; }
        }
    }
}
