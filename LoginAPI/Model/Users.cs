using System.ComponentModel.DataAnnotations;

namespace LoginAPI.Model
{
    public class Users
    {
        [Key]
        public int UserID { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public DateTime DOB { get; set; }
        public string? Gender { get; set; }
        public Int64 PhoneNumber { get; set; }
        public string? Address { get; set; }
        public int? Pin { get; set; }
        public string? ProfilePhotoPath { get; set; }
    }
}
