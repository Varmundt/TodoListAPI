using Microsoft.EntityFrameworkCore;
using todoAPI.Models;

namespace todoAPI.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    public DbSet<User> Users { get; set; }
    public DbSet<TodoItem> TodoItems { get; set; }
}