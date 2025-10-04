using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;

public class UserApiTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public UserApiTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetUsers_ReturnsOkAndUsers()
    {
        // Act
        var response = await _client.GetAsync("/api/users");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var users = await response.Content.ReadFromJsonAsync<List<UserDto>>();
        Assert.NotNull(users);
        Assert.NotEmpty(users);

        foreach (var user in users)
        {
            Assert.False(string.IsNullOrEmpty(user.FirstName));
            Assert.False(string.IsNullOrEmpty(user.LastName));
            Assert.False(string.IsNullOrEmpty(user.UserName));
            Assert.NotNull(user.Visions);

            foreach (var vision in user.Visions)
            {
                Assert.True(vision.VisionLeftEye >= 0);
                Assert.True(vision.VisionRightEye >= 0);
                Assert.True(vision.CylinderLeftEye >= 0);
                Assert.True(vision.CylinderRightEye >= 0);
            }
        }
    }

    [Fact]
    public async Task CreateUser_ReturnsCreatedUser()
    {
        var newUser = new
        {
            FirstName = "Test",
            LastName = "User",
            UserName = "testuser123",
            Password = "test123",
            DateOfBirth = DateTime.UtcNow.AddYears(-20)
        };

        var response = await _client.PostAsJsonAsync("/api/users", newUser);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        var createdUser = await response.Content.ReadFromJsonAsync<UserDto>();
        Assert.NotNull(createdUser);
        Assert.True(createdUser.Id > 0);
        Assert.Equal(newUser.FirstName, createdUser.FirstName);
        Assert.Equal(newUser.LastName, createdUser.LastName);
        Assert.Equal(newUser.UserName, createdUser.UserName);
    }

    [Fact]
    public async Task AddVision_ReturnsUpdatedUser()
    {
        // Arrange: prvo uzmi jednog korisnika
        var users = await _client.GetFromJsonAsync<List<UserDto>>("/api/users");
        var userId = users!.First().Id;

        var newVision = new
        {
            VisionLeftEye = 1.0,
            VisionRightEye = 0.8,
            CylinderLeftEye = 0.1,
            CylinderRightEye = 0.2,
            CreationDate = DateTime.UtcNow
        };

        // Act
        var response = await _client.PostAsJsonAsync($"/api/users/{userId}/visions", newVision);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var updatedUser = await response.Content.ReadFromJsonAsync<UserDto>();
        Assert.NotNull(updatedUser);
        Assert.Contains(updatedUser!.Visions, v =>
            v.VisionLeftEye == newVision.VisionLeftEye &&
            v.VisionRightEye == newVision.VisionRightEye
        );
    }
}

public class UserDto
{
    public int Id { get; set; }
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public string UserName { get; set; } = "";
    public List<UserVisionDto> Visions { get; set; } = new();
}

public class UserVisionDto
{
    public double VisionLeftEye { get; set; }
    public double VisionRightEye { get; set; }
    public double CylinderLeftEye { get; set; }
    public double CylinderRightEye { get; set; }
    public DateTime CreationDate { get; set; }
}
