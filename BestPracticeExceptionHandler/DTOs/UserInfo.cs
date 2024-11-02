namespace BestPracticeExceptionHandler.DTOs;
public readonly record struct UserBaseInfo(
    string UserName,
    string Email,
    string PhoneNumber);

public readonly record struct UserReadInfo(
    int UserId,
    UserBaseInfo UserBaseInfo);

public readonly record struct UserCreateInfo(
    UserBaseInfo UserBaseInfo,
    string Password);

public readonly record struct UserUpdateInfo(
    int UserId,
    UserBaseInfo UserBaseInfo);