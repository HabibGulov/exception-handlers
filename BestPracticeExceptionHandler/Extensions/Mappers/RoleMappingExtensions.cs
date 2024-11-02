namespace BestPracticeExceptionHandler.Extensions.Mappers;

public static class RoleMappingExtensions
{
    public static RoleReadInfo ToReadInfo(this Role role)
    {
        return new()
        {
            Id = role.Id,
            RoleBaseInfo = new()
            {
                Name = role.Name
            }
        };
    }

    public static Role ToRole(this RoleCreateInfo createInfo)
    {
        return new()
        {
            Name = createInfo.RoleBaseInfo.Name,
        };
    }

    public static Role ToUpdatedRole(this Role role, RoleUpdateInfo updateInfo)
    {
        role.Version++;
        role.UpdatedAt = DateTime.UtcNow;
        role.Name = updateInfo.RoleBaseInfo.Name;
        return role;
    }

    public static Role ToDeletedRole(this Role role)
    {
        role.IsDeleted = true;
        role.DeletedAt = DateTime.UtcNow;
        role.UpdatedAt = DateTime.UtcNow;
        role.Version++;
        return role;
    }
}