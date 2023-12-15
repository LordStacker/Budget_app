using System.ComponentModel.DataAnnotations;
namespace infrastructure.DataModels;

public class MyUser
{
    public int Id { get; set; }

    [Required]
    public string UserEmail { get; set; }

    [Required]
    public int UserRole { get; set; }

    [Required]
    public string Username { get; set; }

    [Required]
    public string Firstname { get; set; }

    [Required]
    public string Lastname { get; set; }
}