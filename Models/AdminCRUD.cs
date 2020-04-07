using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EurocomV2.Models
{
    public class AdminCRUD
    {
        private int _ID;
        private string _Name;
        private string _LastName;
        private string _Specialty;
        private int _TreatTeamID;


        public int ID
        {
            get { return this._ID; }
            set { _ID = value; }
        }

        public string Name
        {
            get { return this._Name; }
            set { _Name = value; }
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

        public int TreatTeamID
        {
            get { return this._TreatTeamID; }
            set { _TreatTeamID = value; }
        }

    }
}
