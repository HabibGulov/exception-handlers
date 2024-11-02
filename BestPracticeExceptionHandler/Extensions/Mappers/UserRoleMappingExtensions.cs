namespace BestPracticeExceptionHandler.Extensions.Mappers;

public static class UserRoleMappingExtensions
{
    public static UserRoleReadInfo ToReadInfo(this UserRole userRole)
    {
        return new UserRoleReadInfo()
        {
            Id = userRole.Id,
            RoleReadInfo = userRole.Role.ToReadInfo(),
            UserReadInfo = userRole.User.ToReadInfo(),
            UserRoleBaseInfo = new()
            {
                RoleId = userRole.RoleId,
                UserId = userRole.UserId,
            }
        };
    }

    public static UserRole ToUserRole(this UserRoleCreateInfo createInfo)
    {
        return new()
        {
            RoleId = createInfo.UserRoleBaseInfo.RoleId,
            UserId = createInfo.UserRoleBaseInfo.UserId,
        };
    }

    public static UserRole ToUpdatedUserRole(this UserRole userRole, UserRoleUpdateInfo updateInfo)
    {
        userRole.RoleId = updateInfo.UserRoleBaseInfo.RoleId;
        userRole.UserId = updateInfo.UserRoleBaseInfo.UserId;
        userRole.UpdatedAt = DateTime.UtcNow;
        userRole.Version++;
        return userRole;
    }

    public static UserRole ToDeletedUserRole(this UserRole userRole)
    {
        userRole.DeletedAt = DateTime.UtcNow;
        userRole.IsDeleted = true;
        userRole.UpdatedAt = DateTime.UtcNow;
        userRole.Version++;
        return userRole;
    }
}