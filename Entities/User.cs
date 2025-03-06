namespace TodoApi.Entities;
public class User
{
    public Guid Id { get; set; }
    public string? Username { get; set; }
    public string? PasswordHash { get; set; }
    public IList<Todo> Todos { get; set; } = new List<Todo>();
}