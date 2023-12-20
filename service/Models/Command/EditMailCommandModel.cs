using System.ComponentModel.DataAnnotations;

namespace service.Models.Command;

public class EditMailCommandModel
{
    [Required]
    public string NewEmail { get; set; }
    
    [Required]
    public string OldEmail { get; set; }
    
    [Required]
    public string Password { get; set; }
}