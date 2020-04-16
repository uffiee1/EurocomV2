using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EurocomV2.Models
{
    public class RegisterViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }

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
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }

        [Required]
        public char gender { get; set; }
    }
}
