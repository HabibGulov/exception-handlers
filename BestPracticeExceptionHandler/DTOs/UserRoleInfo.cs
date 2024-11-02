namespace BestPracticeExceptionHandler.DTOs;

public readonly record struct UserRoleBaseInfo(
    int UserId,
    int RoleId);

public readonly record struct UserRoleReadInfo(
    int Id,
    UserRoleBaseInfo UserRoleBaseInfo,
    UserReadInfo UserReadInfo,
    RoleReadInfo RoleReadInfo);

public readonly record struct UserRoleCreateInfo(
    UserRoleBaseInfo UserRoleBaseInfo);

public readonly record struct UserRoleUpdateInfo(
    int Id,
    UserRoleBaseInfo UserRoleBaseInfo);