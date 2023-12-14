using System.ComponentModel.DataAnnotations;

namespace service.Models.Command;

public class UpdateStartAmountCommand
{
    [Required]
    public float updatedStartAmount { get; set; }
}