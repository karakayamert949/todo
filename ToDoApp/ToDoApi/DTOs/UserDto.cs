using System.ComponentModel.DataAnnotations;

namespace ToDoApi;

// For creating or updating a user
    public class UserCreateDto
    {
        [Required(ErrorMessage = "Username is required")]
        public required string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public required string Password { get; set; }
    }

    // For returning user data
    public class UserDto
    {
        public int Id { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
        public bool IsAdmin { get; set; }
    }
