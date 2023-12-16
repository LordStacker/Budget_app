namespace service.Models.Command;

public class UpdateUserCommandModel
{
    public string UserEmail { get; set; }
    
    public string ProfilePhoto { get; set; }
    
    public string Username { get; set; }
    
    public string Firstname { get; set; }
    
    public string Lastname { get; set; }
    
    public string Education { get; set; }

    public DateTime BirthDate { get; set; }
}