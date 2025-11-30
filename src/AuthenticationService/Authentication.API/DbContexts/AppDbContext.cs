using Authentication.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Authentication.API.DbContexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Explicitly map the User entity to the "Users" table
        modelBuilder.Entity<User>().ToTable("Users");
        
        base.OnModelCreating(modelBuilder);
    }
}