using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LoginApp.Models
{
    public class UserModel
    {
        [Key]

        public int UserID { get; set;}

        [Required]
        [Display(Name = "First Name")]
        public string? FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string? LastName { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        public DateTime DOB { get; set; }

        [Required]
        public string? Gender { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [RegularExpression(@"^\d{6}$", ErrorMessage = "Enter a valid 6-digit PIN")]
        public string Pin { get; set; }

        [Display(Name = "Profile Photo")]
        public string ProfilePhotoPath { get; set; }
    }
}
