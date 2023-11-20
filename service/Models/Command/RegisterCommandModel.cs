using System.ComponentModel.DataAnnotations;

namespace service.Models.Command
{
    public class RegisterCommandModel
    {
        [Required]
        public string UserEmail { get; set; }

        [Required]
        [MinLength(8)]
        public string Password { get; set; }

        public string ProfilePhoto { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Firstname { get; set; }

        [Required]
        public string Lastname { get; set; }

        [Required]
        public string Education { get; set; }

        public DateTime BirthDate { get; set; }

    }
}