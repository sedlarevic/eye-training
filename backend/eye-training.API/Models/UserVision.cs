using Microsoft.EntityFrameworkCore;

namespace eye_training.API.Models;

public class UserVision
{
    public int Id { get; set; }
    [Precision(5, 2)]
    public decimal VisionLeftEye { get; set; }

    [Precision(5, 2)]
    public decimal VisionRightEye { get; set; }

    [Precision(5, 2)]
    public decimal CylinderLeftEye { get; set; }

    [Precision(5, 2)]
    public decimal CylinderRightEye { get; set; }

    public DateTime CreationDate { get; set; }

    public int UserId { get; set; }
    public User User { get; set; } = null!;
}
