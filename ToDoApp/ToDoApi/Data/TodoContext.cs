using Microsoft.EntityFrameworkCore;

namespace ToDoApi;

public class TodoContext : DbContext
{
    public TodoContext(DbContextOptions<TodoContext> options) : base(options) { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Seeding initial data
        modelBuilder.Entity<UserRole>().HasData(
            new UserRole { Id = 1, Role = "Admin" },
            new UserRole { Id = 2, Role = "User" }
        );

        modelBuilder.Entity<User>().HasData(
            new User{Id=1,Email="test@gmail.com",Username="admin",PasswordHash=BCrypt.Net.BCrypt.HashPassword("123456"),RoleId=2}
        );
    }

    public DbSet<User> MK_Users { get; set; }
    public DbSet<Todo> MK_Todos { get; set; }
    public DbSet<UserRole> MK_UserRoles { get; set; }
}
