namespace aspnetcore.ntier.DAL.Entities;

public class User
{
    public int UserId { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public bool IsAdmin { get; set; }
    public ICollection<ShortUrl> ShortUrls { get; set; }

    public ICollection<UserRole> UserRoles { get; set; }
}
