using Microsoft.EntityFrameworkCore;
using eye_training.API.Models;

namespace eye_training.API.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<UserVision> UserVisions { get; set; }
}
