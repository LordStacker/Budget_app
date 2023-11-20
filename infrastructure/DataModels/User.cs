using System.ComponentModel.DataAnnotations;

namespace infrastructure.DataModels
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string UserEmail { get; set; }

        [Required]
        public string Hash { get; set; }

        [Required]
        public int UserRole { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Firstname { get; set; }

        [Required]
        public string Lastname { get; set; }

        [Required]
        public string Education { get; set; }

        public DateTime? BirthDate { get; set; }

        public bool IsAdmin { get; set; }

        public string ProfilePhoto { get; set; }
    }
}