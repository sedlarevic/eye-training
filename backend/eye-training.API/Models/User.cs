namespace eye_training.API.Models;

public class User
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public DateTime DateOfBirth { get; set; }
    public string UserName { get; set; } = null!;
    public string Password { get; set; } = null!;

    public ICollection<UserVision> Visions { get; set; } = new List<UserVision>();
}
