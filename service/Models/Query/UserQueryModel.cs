namespace service.Query;

public class UserQueryModel
{
    public required int Id { get; set; }
    public required string Email { get; set; }
    public required int Role { get; set; }
    public required string Username { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public string? Education { get; set; }
    public DateTime? Birthday { get; set; }
    public string? ProfilePhoto { get; set; }
}