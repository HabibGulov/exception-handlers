namespace BestPracticeExceptionHandler.Entities;

public sealed class Role : BaseEntity
{
    public string Name { get; set; } = null!;
    public ICollection<UserRole> UserRoles { get; set; } = [];
}