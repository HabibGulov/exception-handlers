namespace BestPracticeExceptionHandler.Extensions.Mappers;

public static class UserMappingExtensions
{
    public static UserReadInfo ToReadInfo(this User user)
    {
        return new(
            user.Id,
            new UserBaseInfo(
                user.UserName,
                user.Email,
                user.PhoneNumber)
        );
    }

    public static User ToUser(this UserCreateInfo createInfo)
    {
        return new()
        {
            UserName = createInfo.UserBaseInfo.UserName,
            Email = createInfo.UserBaseInfo.Email,
            PhoneNumber = createInfo.UserBaseInfo.PhoneNumber,
            Password = createInfo.Password,
        };
    }

    public static User ToUpdatedUser(this User user, UserUpdateInfo updateInfo)
    {
        user.UpdatedAt = DateTime.UtcNow;
        user.Version++;
        user.PhoneNumber = updateInfo.UserBaseInfo.PhoneNumber;
        user.UserName = updateInfo.UserBaseInfo.UserName;
        user.Email = updateInfo.UserBaseInfo.Email;
        return user;
    }

    public static User ToDeletedUser(this User user)
    {
        user.IsDeleted = true;
        user.DeletedAt = DateTime.UtcNow;
        user.UpdatedAt = DateTime.UtcNow;
        user.Version++;
        return user;
    }
}