using System.ComponentModel.DataAnnotations;

namespace service.Models.Command;

public class UploadPhotoCommandModel
{
    [Required]
    public string image { get; set; }
}