// В файле User.cs
public class User
{
    public int Id { get; set; }
    public string Username { get; set; } = "";
    public string PasswordHash { get; set; } = "";
    public string FullName { get; set; } = "";
    public string Role { get; set; } = "User";
    public DateTime CreatedAt { get; set; }
}


public class LoginModel
{
    public string Username { get; set; } = ""; 
    public string Password { get; set; } = "";
}