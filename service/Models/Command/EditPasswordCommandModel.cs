using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace service.Models.Command;

public class EditPasswordCommandModel
{
    [Required]
    public string NewPassword { get; set; }
    
    [Required]
    public string OldPassword { get; set; }
    
    [Required]
    public string UserEmail { get; set; }   
    
}