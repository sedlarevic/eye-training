using eye_training.API.Data;
using eye_training.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eye_training.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _db;

        public UsersController(AppDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _db.Users
                .Include(u => u.Visions)
                .Select(u => new UserDto
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    UserName = u.UserName,
                    Visions = u.Visions.Select(v => new UserVisionDto
                    {
                        VisionLeftEye = v.VisionLeftEye,
                        VisionRightEye = v.VisionRightEye,
                        CylinderLeftEye = v.CylinderLeftEye,
                        CylinderRightEye = v.CylinderRightEye,
                        CreationDate = v.CreationDate
                    }).ToList()
                })
                .ToListAsync();

            return Ok(users);

        }
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = new User
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                UserName = dto.UserName,
                Password = dto.Password,
                DateOfBirth = dto.DateOfBirth
            };

            _db.Users.Add(user);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUsers), new { id = user.Id }, user);
        }

        [HttpPost("{userId}/visions")]
        public async Task<IActionResult> AddVision(int userId, [FromBody] AddUserVisionDto dto)
        {
            var user = await _db.Users
                .Include(u => u.Visions)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null) return NotFound();

            var vision = new UserVision
            {
                UserId = userId,
                VisionLeftEye = dto.VisionLeftEye,
                VisionRightEye = dto.VisionRightEye,
                CylinderLeftEye = dto.CylinderLeftEye,
                CylinderRightEye = dto.CylinderRightEye,
                CreationDate = DateTime.UtcNow
            };

            _db.UserVisions.Add(vision);
            await _db.SaveChangesAsync();

            await _db.Entry(user).Collection(u => u.Visions).LoadAsync();

            var userDto = new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Visions = user.Visions.Select(v => new UserVisionDto
                {
                    VisionLeftEye = v.VisionLeftEye,
                    VisionRightEye = v.VisionRightEye,
                    CylinderLeftEye = v.CylinderLeftEye,
                    CylinderRightEye = v.CylinderRightEye,
                    CreationDate = v.CreationDate
                }).ToList()
            };

            return Ok(userDto);
        }
    }
}
