namespace eye_training.API;

public class UserVisionDto
{
    public decimal VisionLeftEye { get; set; }
    public decimal VisionRightEye { get; set; }
    public decimal CylinderLeftEye { get; set; }
    public decimal CylinderRightEye { get; set; }
    public DateTime CreationDate { get; set; }
}
