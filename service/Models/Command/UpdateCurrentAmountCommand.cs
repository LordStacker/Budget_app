using System.ComponentModel.DataAnnotations;

namespace service.Models.Command;

public class UpdateCurrentAmountCommand
{
    [Required] public float NewCurrentAmount { get; set; }
}