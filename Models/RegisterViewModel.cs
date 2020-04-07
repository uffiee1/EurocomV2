using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EurocomV2.Models
{
    //Applying validation in UserModel. (Laag bij laag)
    [MetadataType(typeof(UserRegisteration))]

    public class RegisterViewModel
    {
        //Omdat dit niet in actuele model zit, moet ik hier ook toevoegen.
        //Laat zien in register page maar saved niet in database.
        public string ConfirmPassword { get; set; }
    }

    public class UserRegisteration
    {
        private string _FirstName;
        private string _LastName;
        private string _Username;
        private string _Email;
        private string _Password;
        private string _ConfirmPassword;
        private DateTime _DateOfBirth;
         

        [Display(Name ="Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage ="First Name required")]
        public string FirstName
        {
            get { return this._FirstName; }
            set { _FirstName = value; }
        }

        [Display(Name = "Last Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Last Name required")]
        public string LastName
        {
            get { return this._LastName; }
            set { _LastName = value; }
        }

        [Display(Name = "Username")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Username required")]
        public string Username
        {
            get { return this._Username; }
            set { _Username = value; }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Email adress required")]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string Email
        {
            get { return this._Email; }
            set { _Email = value; }
        }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(6,ErrorMessage ="Minimum 6 characters required")]
        public string Password
        {
            get { return this._Password; }
            set { _Password = value; }
        }

        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword
        {
            get { return this._ConfirmPassword; }
            set { _ConfirmPassword = value; }
        }

        [Display(Name = "Date of birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime DateOfBirth
        {
            get { return this._DateOfBirth; }
            set { _DateOfBirth = value; }
        }
    }
}
