namespace BestPracticeExceptionHandler.Entities;

public sealed class User : BaseEntity
{
    public string UserName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string Password { get; set; } = null!;
    public ICollection<UserRole> UserRoles { get; set; } = [];
}