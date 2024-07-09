namespace ToDoApi;

public class User
{
    public int Id { get; set; }
    public required string Email { get; set; }
    public required string Username { get; set; }
    public required string PasswordHash { get; set; }
    public int RoleId { get; set; }=2;
}
